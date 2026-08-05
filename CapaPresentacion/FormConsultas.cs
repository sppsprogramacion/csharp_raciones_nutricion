using CapaDatos;
using CapaNegocio;
using CapaPresentacion.FuncionesGenerales;
using CapaPresentacion.Reportes;
using ClosedXML.Excel;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace CapaPresentacion
{
    public partial class FormConsultas : Form
    {
        public FormConsultas()
        {
            InitializeComponent();
        }

        private void FormConsultas_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            //obtener lista elaborada por fecha
            var nRacionElaborada = new NRacionElaborada();

            var (listaRacionElaboradas, error) = nRacionElaborada.ListaXFecha(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionElaboradas.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionElaboradas = listaRacionElaboradas
                .OrderBy(s => s.fecha_elaborada)
                .ToList();
            //fin obtener lista elaboradas

            //obtener lista solicitadas por fecha
            var nRacionSolicitada = new NRacionSolicitada();

            var (listaRacionSolicitada, errorSolicitada) = nRacionSolicitada.ListaXFecha(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionSolicitada.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionSolicitada = listaRacionSolicitada
                .OrderBy(s => s.fecha_solicitada)
                .ToList();
            //fin obtener lista solicitadas

            var datos = listaRacionElaboradas
                .OrderBy(r => r.fecha_elaborada)
                .SelectMany(r => r.raciones_elaboradas_detalles.Select(d => new
                {
                    Id = r.id_racion_elaborada,
                    Fecha = r.fecha_elaborada,
                    UnidadOrden = d.unidad.orden,
                    SapOrden = d.sap.orden,
                    TipoMenuOrden = d.tipo_menu.orden,
                    Unidad = d.unidad.unidad,
                    Sap = d.sap.sap,
                    TipoMenu = d.tipo_menu.tipo_menu,
                    Almuerzo = d.almuerzo,
                    Cena = d.cena
                }))
                .OrderBy(x => x.Fecha)
                .ThenBy(x => x.UnidadOrden)
                .ThenBy(x => x.SapOrden)
                .ThenBy(x => x.TipoMenuOrden)
                .ToList();

            dtgResultado.DataSource = datos;
            dtgResultado.Columns["UnidadOrden"].Visible = false;
            dtgResultado.Columns["SapOrden"].Visible = false;
            dtgResultado.Columns["TipoMenuOrden"].Visible = false;

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            //obtener lista elaborada por fecha
            var nRacionElaborada = new NRacionElaborada();

            var (listaRacionElaboradas, error) = nRacionElaborada.ListaXFecha(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionElaboradas.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionElaboradas = listaRacionElaboradas
                .OrderBy(s => s.fecha_elaborada)
                .ToList();
            //fin obtener lista elaboradas

            //obtener lista solicitadas por fecha
            var nRacionSolicitada = new NRacionSolicitada();

            var (listaRacionSolicitada, errorSolicitada) = nRacionSolicitada.ListaXFecha(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionSolicitada.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionSolicitada = listaRacionSolicitada
                .OrderBy(s => s.fecha_solicitada)
                .ToList();
            //fin obtener lista solicitadas

            var datos = listaRacionElaboradas
                .OrderBy(r => r.fecha_elaborada)
                .SelectMany(r => r.raciones_elaboradas_detalles.Select(d => new
                {
                    Id = r.id_racion_elaborada,
                    Fecha = r.fecha_elaborada,
                    UnidadOrden = d.unidad.orden,
                    SapOrden = d.sap.orden,
                    TipoMenuOrden = d.tipo_menu.orden,
                    Unidad = d.unidad.unidad,
                    Sap = d.sap.sap,
                    TipoMenu = d.tipo_menu.tipo_menu,
                    Almuerzo = d.almuerzo,
                    Cena = d.cena
                }))
                .OrderBy(x => x.Fecha)
                .ThenBy(x => x.UnidadOrden)
                .ThenBy(x => x.SapOrden)
                .ThenBy(x => x.TipoMenuOrden)
                .ToList();

            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Raciones");

                // Encabezados
                ws.Cell(1, 1).Value = "ID";
                ws.Cell(1, 2).Value = "Fecha";
                ws.Cell(1, 3).Value = "Unidad";
                ws.Cell(1, 4).Value = "SAP";
                ws.Cell(1, 5).Value = "Tipo Menú";
                ws.Cell(1, 6).Value = "Almuerzo";
                ws.Cell(1, 7).Value = "Cena";

                int fila = 2;

                foreach (var item in datos)
                {
                    ws.Cell(fila, 1).Value = item.Id;
                    ws.Cell(fila, 2).Value = item.Fecha;
                    ws.Cell(fila, 3).Value = item.Unidad;
                    ws.Cell(fila, 4).Value = item.Sap;
                    ws.Cell(fila, 5).Value = item.TipoMenu;
                    ws.Cell(fila, 6).Value = item.Almuerzo;
                    ws.Cell(fila, 7).Value = item.Cena;

                    fila++;
                }

                ws.Columns().AdjustToContents();

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
                    sfd.Title = "Guardar archivo Excel";
                    sfd.FileName = $"Raciones_{DateTime.Now:yyyyMMdd}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        wb.SaveAs(sfd.FileName);

                        MessageBox.Show("Archivo exportado correctamente.", "Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }


        private void btnImrpimirParteDiario_Click(object sender, EventArgs e)
        {
            //obtener lista elaborada por fecha
            var nRacionElaborada = new NRacionElaborada();

            var (listaRacionElaboradas, error) = nRacionElaborada.ListaXFecha(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionElaboradas.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionElaboradas = listaRacionElaboradas
                .OrderBy(s => s.fecha_elaborada)
                .ToList();
            //fin obtener lista elaboradas

            //obtener lista solicitadas por fecha
            var nRacionSolicitada = new NRacionSolicitada();

            var (listaRacionSolicitada, errorSolicitada) = nRacionSolicitada.ListaXFecha(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionSolicitada.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionSolicitada = listaRacionSolicitada
                .OrderBy(s => s.fecha_solicitada)
                .ToList();
            //fin obtener lista solicitadas

            //Listar Unidades
            NUnidad nUnidad = new NUnidad();
            (List<DUnidad> listaUnidades, string errorResponseUnidad) = nUnidad.ListarTodos();

            if (errorResponseUnidad != null)
            {
                MessageBox.Show(errorResponseUnidad, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaUnidades = listaUnidades
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar Unidades

            //Listar Sap
            NSap nSap = new NSap();
            (List<DSap> listaSap, string errorResponseSap) = nSap.ListarTodos();

            if (errorResponseSap != null)
            {
                MessageBox.Show(errorResponseSap, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaSap = listaSap
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar Sap

            //Listar TipoMenu
            NTipoMenu nTipoMenu = new NTipoMenu();
            (List<DTipoMenu> listaTipoMenu, string errorResponseTipoMenu) = nTipoMenu.ListarTodos();

            if (errorResponseTipoMenu != null)
            {
                MessageBox.Show(errorResponseTipoMenu, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaTipoMenu = listaTipoMenu
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar TipoMenu

            //Listar OBSERVACIONES
            NObservaciones nObservaciones = new NObservaciones();
            (List<DObservacionGeneral> listaObservacionesGenerales, string errorResponseObservaciones) = nObservaciones.ListarTodosGeneral();

            if (errorResponseObservaciones != null)
            {
                MessageBox.Show(errorResponseObservaciones, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaObservacionesGenerales = listaObservacionesGenerales
                .OrderBy(s => s.id_observacion_general)
                .ToList();
            //fin Listar OBSERVACIONES

            // Generar PDF en memoria
            MemoryStream msOriginal = null;

            msOriginal = ReportesParteDiarioPDF.RepPdfParteDiario(listaRacionElaboradas, listaRacionSolicitada, listaUnidades, listaSap, listaTipoMenu, listaObservacionesGenerales, dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            // Clonar el stream para que PdfiumViewer pueda cerrarlo sin afectar el original
            MemoryStream ms = new MemoryStream(msOriginal.ToArray());

            PdfDocument pdfDocument = null;

            try
            {
                pdfDocument = PdfDocument.Load(ms);

                Form formVisor = new Form
                {
                    Text = "Vista previa PDF",
                    Width = 800,
                    Height = 600
                };

                PdfViewer pdfViewer = new PdfViewer
                {
                    Dock = DockStyle.Fill,
                    Document = pdfDocument
                };

                formVisor.Controls.Add(pdfViewer);

                formVisor.FormClosed += (s, args) =>
                {
                    // Liberar recursos al cerrar el visor
                    pdfViewer.Document.Dispose();
                    pdfViewer.Dispose();
                    formVisor.Dispose();
                    ms.Dispose();
                    pdfDocument = null;
                };

                formVisor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar PDF: " + ex.Message);
                ms.Dispose();
                pdfDocument?.Dispose();
            }
        }
                
        private void btnImprimirEstadistico_Click(object sender, EventArgs e)
        {
            //obtener lista elaborada por fecha
            var nRacionElaborada = new NRacionElaborada();

            var (listaRacionElaboradas, error) = nRacionElaborada.ListaXFecha(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionElaboradas.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionElaboradas = listaRacionElaboradas
                .OrderBy(s => s.fecha_elaborada)
                .ToList();
            //fin obtener lista elaboradas

            //obtener lista solicitadas por fecha
            var nRacionSolicitada = new NRacionSolicitada();

            var (listaRacionSolicitada, errorSolicitada) = nRacionSolicitada.ListaXFecha(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionSolicitada.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionSolicitada = listaRacionSolicitada
                .OrderBy(s => s.fecha_solicitada)
                .ToList();
            //fin obtener lista solicitadas

            //Listar Unidades
            NUnidad nUnidad = new NUnidad();
            (List<DUnidad> listaUnidades, string errorResponseUnidad) = nUnidad.ListarTodos();

            if (errorResponseUnidad != null)
            {
                MessageBox.Show(errorResponseUnidad, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaUnidades = listaUnidades
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar Unidades

            //Listar Sap
            NSap nSap = new NSap();
            (List<DSap> listaSap, string errorResponseSap) = nSap.ListarTodos();

            if (errorResponseSap != null)
            {
                MessageBox.Show(errorResponseSap, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaSap = listaSap
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar Sap

            //Listar TipoMenu
            NTipoMenu nTipoMenu = new NTipoMenu();
            (List<DTipoMenu> listaTipoMenu, string errorResponseTipoMenu) = nTipoMenu.ListarTodos();

            if (errorResponseTipoMenu != null)
            {
                MessageBox.Show(errorResponseTipoMenu, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaTipoMenu = listaTipoMenu
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar TipoMenu


            // Generar PDF en memoria
            MemoryStream msOriginal = null;

            msOriginal = ReportesParteDiarioPDF.RepPdfEstadistico(listaRacionElaboradas, listaSap, listaTipoMenu, dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            // Clonar el stream para que PdfiumViewer pueda cerrarlo sin afectar el original
            MemoryStream ms = new MemoryStream(msOriginal.ToArray());

            PdfDocument pdfDocument = null;

            try
            {
                pdfDocument = PdfDocument.Load(ms);

                Form formVisor = new Form
                {
                    Text = "Vista previa PDF",
                    Width = 800,
                    Height = 600
                };

                PdfViewer pdfViewer = new PdfViewer
                {
                    Dock = DockStyle.Fill,
                    Document = pdfDocument
                };

                formVisor.Controls.Add(pdfViewer);

                formVisor.FormClosed += (s, args) =>
                {
                    // Liberar recursos al cerrar el visor
                    pdfViewer.Document.Dispose();
                    pdfViewer.Dispose();
                    formVisor.Dispose();
                    ms.Dispose();
                    pdfDocument = null;
                };

                formVisor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar PDF: " + ex.Message);
                ms.Dispose();
                pdfDocument?.Dispose();
            }
        }

        private void btnImprimirParteDiarioNovedades_Click(object sender, EventArgs e)
        {
            //obtener lista elaborada por fecha
            var nRacionElaborada = new NRacionElaborada();

            var (listaRacionElaboradas, error) = nRacionElaborada.ListaXFecha(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionElaboradas.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionElaboradas = listaRacionElaboradas
                .OrderBy(s => s.fecha_elaborada)
                .ToList();
            //fin obtener lista elaboradas

            //Listar Unidades
            NUnidad nUnidad = new NUnidad();
            (List<DUnidad> listaUnidades, string errorResponseUnidad) = nUnidad.ListarTodos();

            if (errorResponseUnidad != null)
            {
                MessageBox.Show(errorResponseUnidad, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaUnidades = listaUnidades
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar Unidades

            //Listar Unidades grupo
            NUnidadGrupo nUnidadGrupo = new NUnidadGrupo();
            (List<DUnidadGrupo> listaUnidadesGrupo, string errorResponseUnidadGrupo) = nUnidadGrupo.ListarTodos();

            if (errorResponseUnidadGrupo != null)
            {
                MessageBox.Show(errorResponseUnidadGrupo, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaUnidadesGrupo = listaUnidadesGrupo
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar Unidades grupo


            //Listar TipoMenu
            NTipoMenu nTipoMenu = new NTipoMenu();
            (List<DTipoMenu> listaTipoMenu, string errorResponseTipoMenu) = nTipoMenu.ListarTodos();

            if (errorResponseTipoMenu != null)
            {
                MessageBox.Show(errorResponseTipoMenu, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaTipoMenu = listaTipoMenu
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar TipoMenu


            // Generar PDF en memoria
            MemoryStream msOriginal = null;

            msOriginal = ReportesParteDiarioPDF.RepPdfParteNovedadesDiario(listaRacionElaboradas, listaUnidadesGrupo, listaUnidades, listaTipoMenu, dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            // Clonar el stream para que PdfiumViewer pueda cerrarlo sin afectar el original
            MemoryStream ms = new MemoryStream(msOriginal.ToArray());

            PdfDocument pdfDocument = null;

            try
            {
                pdfDocument = PdfDocument.Load(ms);

                Form formVisor = new Form
                {
                    Text = "Vista previa PDF",
                    Width = 800,
                    Height = 600
                };

                PdfViewer pdfViewer = new PdfViewer
                {
                    Dock = DockStyle.Fill,
                    Document = pdfDocument
                };

                formVisor.Controls.Add(pdfViewer);

                formVisor.FormClosed += (s, args) =>
                {
                    // Liberar recursos al cerrar el visor
                    pdfViewer.Document.Dispose();
                    pdfViewer.Dispose();
                    formVisor.Dispose();
                    ms.Dispose();
                    pdfDocument = null;
                };

                formVisor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar PDF: " + ex.Message);
                ms.Dispose();
                pdfDocument?.Dispose();
            }
        }


        //AGREGAR FILA TOTALES UNIDAD
        private void AgregarFilaTotales(List<DUnidadMenuCantidades> lista)
        {
            // Evitar duplicar la fila Totales
            lista.RemoveAll(x => x.unidad == "Totales");

            var totales = new DUnidadMenuCantidades
            {
                unidad = "Totales",

                P12_A = lista.Sum(x => x.P12_A),
                P12_C = lista.Sum(x => x.P12_C),

                P24_A = lista.Sum(x => x.P24_A),
                P24_C = lista.Sum(x => x.P24_C),

                IntN_A = lista.Sum(x => x.IntN_A),
                IntN_C = lista.Sum(x => x.IntN_C),

                Astr_A = lista.Sum(x => x.Astr_A),
                Astr_C = lista.Sum(x => x.Astr_C),

                Celi_A = lista.Sum(x => x.Celi_A),
                Celi_C = lista.Sum(x => x.Celi_C),

                AFib_A = lista.Sum(x => x.AFib_A),
                AFib_C = lista.Sum(x => x.AFib_C),

                Hep_A = lista.Sum(x => x.Hep_A),
                Hep_C = lista.Sum(x => x.Hep_C),

                SSal_A = lista.Sum(x => x.SSal_A),
                SSal_C = lista.Sum(x => x.SSal_C),

                HivTbc_A = lista.Sum(x => x.HivTbc_A),
                HivTbc_C = lista.Sum(x => x.HivTbc_C),

                Men_A = lista.Sum(x => x.Men_A),
                Men_C = lista.Sum(x => x.Men_C),

                SobreAl_A = lista.Sum(x => x.SobreAl_A),
                SobreAl_C = lista.Sum(x => x.SobreAl_C),
            };

            lista.Add(totales);

            int totalAux = totales.P12_A + totales.P12_C + totales.P24_A + totales.P24_C + totales.IntN_A + totales.IntN_C
                + totales.Astr_A + totales.Astr_C + totales.Celi_A + totales.Celi_C + totales.AFib_A + totales.AFib_C
                + totales.Hep_A + totales.Hep_C + totales.SSal_A + totales.SSal_C + totales.HivTbc_A + totales.HivTbc_C
                + totales.Men_A + totales.Men_C;

            //txtTotal.Text = totalAux.ToString();

        } 
        // FIN AGREGAR FILA TOTALES UNIDAD............................................................

    }
}
