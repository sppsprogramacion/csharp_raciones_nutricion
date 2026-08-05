using CapaDatos;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaNegocio;
using System.Runtime.CompilerServices;
using DocumentFormat.OpenXml.Bibliography;
using System.Windows.Forms;

namespace CapaPresentacion.Reportes
{
    public class ReportesParteDiarioPDF
    {

        // PARTE DIARIO NUEVO
        public static MemoryStream RepPdfParteDiario(List<DRacionElaborada> listaRacionElaboradas, List<DRacionSolicitada> listaRacionSolicitadas, List<DUnidad> listaUnidades, List<DSap> listaSap, List<DTipoMenu> listaTipoMenu,List<DObservacionGeneral> listaObservacionesGenerales, string fechaInicio, string fechaFin)
        {
            MemoryStream ms = new MemoryStream();

            Document doc = new Document(PageSize.A4.Rotate(), 8, 8, 5, 5);

            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            writer.CloseStream = false;

            doc.Open();

            DateTime inicio = Convert.ToDateTime(fechaInicio);
            DateTime fin = Convert.ToDateTime(fechaFin);

            //AGREGAR OBSERVACIONES GENERALES 
            AgregarPaginaObsGeneralesParteDiario(doc, listaObservacionesGenerales, inicio, fin );
            // --------------------------------- Nueva página ----------------------------------------------
            doc.NewPage();

            //INICIO DE CREACION DE PAGINAS DE RACIONES CARGADAS
            for (DateTime fecha = inicio; fecha <= fin; fecha = fecha.AddDays(1))
            {

                //GENERAR paginas de solicitadas: unidad y sap
                DRacionSolicitada racionSolicitada = listaRacionSolicitadas.Where(x => x.fecha_solicitada == fecha).First();
                List<DRacionesSolicitadasDetalles> listaDetallesSolicitada = racionSolicitada.raciones_solicitadas_detalles.ToList();
                List<DObservacionSolicitada> listaObservacionesSolicitadas = racionSolicitada.observaciones_solicitada.ToList();


                List<DUnidadMenuCantidades> listaUnidadesCantidadesSolicitadas = new List<DUnidadMenuCantidades>();
                List<DSapMenuCantidades> listaSapCantidadesSolicitadas = new List<DSapMenuCantidades>();
                listaUnidadesCantidadesSolicitadas = GenerarListaUnidadesCantidadesSolicitadas(listaDetallesSolicitada, listaUnidades, listaTipoMenu);
                listaSapCantidadesSolicitadas = GenerarListaSapCantidadesSolicitadas(listaDetallesSolicitada, listaSap, listaTipoMenu);
                //agregar una pagina al documento : Solicitadas unidades
                AgregarPagina(doc, listaUnidadesCantidadesSolicitadas, null, null, null, racionSolicitada.fecha_solicitada, "SOLICITADAS", "Unidades\nCarcelarias");
                // --------------------------------- Nueva página ----------------------------------------------
                doc.NewPage();
                //agregar una pagina al documento : Solicitas sap
                AgregarPagina(doc, null, listaSapCantidadesSolicitadas, null, listaObservacionesSolicitadas,  racionSolicitada.fecha_solicitada, "SOLICITADAS", "Servicios de\nAlimentación");
                // --------------------------------- Nueva página ----------------------------------------------
                doc.NewPage();


                //GENERAR paginas de elaboradas: unidad y sap
                DRacionElaborada racionElaborada = listaRacionElaboradas.Where(x => x.fecha_elaborada == fecha).First();
                List<DRacionElaboradaDetalles> listaDetallesElaboradas = racionElaborada.raciones_elaboradas_detalles.ToList();
                List<DObservacionElaborada> listaObservacionesElaboradas = racionElaborada.observaciones_elaborada.ToList();

                List<DUnidadMenuCantidades> listaUnidadesCantidades = new List<DUnidadMenuCantidades>();
                List<DSapMenuCantidades> listaSapCantidades = new List<DSapMenuCantidades>();

                listaUnidadesCantidades = GenerarListaUnidadesCantidadesElaboradas(listaDetallesElaboradas, listaUnidades, listaTipoMenu);
                listaSapCantidades = GenerarListaSapCantidadesElaboradas(listaDetallesElaboradas, listaSap, listaTipoMenu);
                //agregar una pagina al documento : Elaborada unidades
                AgregarPagina(doc, listaUnidadesCantidades, null, null, null, racionElaborada.fecha_elaborada, "ELABORADAS", "Unidades\nCarcelarias");
                // --------------------------------- Nueva página ----------------------------------------------
                doc.NewPage();
                //agregar una pagina al documento : Elaborada sap
                AgregarPagina(doc, null, listaSapCantidades, listaObservacionesElaboradas, null, racionElaborada.fecha_elaborada, "ELABORADAS", "Servicios de\nAlimentación");
                // --------------------------------- Nueva página ----------------------------------------------
                doc.NewPage();

                AgregarPaginaSapCantidades(doc, writer, listaDetallesElaboradas, racionElaborada.fecha_elaborada, listaSap, listaTipoMenu);

                // --------------------------------- Nueva página ----------------------------------------------
                doc.NewPage();
            }

            

            doc.Close();

            ms.Position = 0;
            
            return ms;
        }

        //FIN PARTE DIARIO NUEVO...................................................................

        // PARTE ESTADISTICO
        public static MemoryStream RepPdfEstadistico(List<DRacionElaborada> listaRacionElaboradas, List<DSap> listaSap, List<DTipoMenu> listaTipoMenu, string fechaInicio, string fechaFin)
        {
            MemoryStream ms = new MemoryStream();

            Document doc = new Document(PageSize.A4.Rotate(), 8, 8, 5, 5);

            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            writer.CloseStream = false;

            doc.Open();


            DateTime inicio = Convert.ToDateTime(fechaInicio);
            DateTime fin = Convert.ToDateTime(fechaFin);

            foreach(DSap sap in listaSap)
            {

                //GENERAR paginas de elaboradas: unidad y sap
                

                List<DSapMenuEstadistico> listaSapCantidades = new List<DSapMenuEstadistico>();

                
                //listaSapCantidades = GenerarListaSapCantidadesElaboradas(listaDetallesElaboradas, listaSap, listaTipoMenu);
                listaSapCantidades = GenerarListaSapEstadisticoElaboradas(listaRacionElaboradas, sap, listaTipoMenu, inicio, fin);
                
                
                //agregar una pagina al documento : Elaborada sap
                AgregarPaginaEstadistico(doc, listaSapCantidades, sap, "ELABORADAS");
                
                // --------------------------------- Nueva página ----------------------------------------------
                doc.NewPage();
            }



            doc.Close();

            ms.Position = 0;

            return ms;
        }

        //FIN PARTE ESTADISTICO...................................................................

        // PARTE NOVEDADES DIARIO
        public static MemoryStream RepPdfParteNovedadesDiario(List<DRacionElaborada> listaRacionElaboradas, List<DUnidadGrupo> listaUnidadesGrupo, List<DUnidad> listaUnidades, List<DTipoMenu> listaTipoMenu, string fechaInicio, string fechaFin)
        {
            MemoryStream ms = new MemoryStream();

            Document doc = new Document(PageSize.A4.Rotate(), 8, 8, 5, 5);

            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            writer.CloseStream = false;

            doc.Open();


            DateTime inicio = Convert.ToDateTime(fechaInicio);
            DateTime fin = Convert.ToDateTime(fechaFin);

            foreach (DUnidadGrupo unidadGrupo in listaUnidadesGrupo)
            {

                //GENERAR paginas de elaboradas: unidad y sap


                List<DUnidadMenuCantidades> listaUnidadCantidades = new List<DUnidadMenuCantidades>();

                List<DUnidad> listafiltroUnidades = listaUnidades.Where(x => x.unidad_grupo_id == unidadGrupo.id_unidad_grupo).ToList();
                int contadorTablas = 0;
                int cantidadTablas = 0;

                if (listafiltroUnidades.Count == 1)
                    cantidadTablas = 6;

                if (listafiltroUnidades.Count == 2 || listafiltroUnidades.Count == 3)
                    cantidadTablas = 5;

                if (listafiltroUnidades.Count == 4 || listafiltroUnidades.Count == 5)
                    cantidadTablas = 4;

                if (listafiltroUnidades.Count == 6 || listafiltroUnidades.Count == 7)
                    cantidadTablas = 3;


                for (DateTime fecha = inicio; fecha <= fin; fecha = fecha.AddDays(1))
                {


                    //listaSapCantidades = GenerarListaSapCantidadesElaboradas(listaDetallesElaboradas, listaSap, listaTipoMenu);
                    listaUnidadCantidades = GenerarListaCantidadesParteNovedadesDiario(listaRacionElaboradas, listafiltroUnidades, listaTipoMenu, fecha);


                    //agregar una pagina al documento : Elaborada sap
                    AgregarPaginaParteNovedades(doc, listaUnidadCantidades, unidadGrupo, "ELABORADAS", fecha.ToShortDateString());
                    
                    contadorTablas++;

                    if(contadorTablas < cantidadTablas)
                    {                        
                        doc.Add(new Paragraph(" "));
                    }
                    else
                    {
                        contadorTablas = 0;

                        doc.NewPage();


                        // --------------------------------- Nueva página ----------------------------------------------
                        //doc.NewPage();
                    }

                }

                doc.NewPage();
            }



            doc.Close();

            ms.Position = 0;

            return ms;
        }

        //FIN PARTE NOVEDADES DIARIO...................................................................

        //METODO GENERAR LISTA UNIDAD CANTIDADES ELABORADAS
        private static List<DUnidadMenuCantidades> GenerarListaUnidadesCantidadesElaboradas(List<DRacionElaboradaDetalles> listaDetalles, List<DUnidad> listaUnidades, List<DTipoMenu> listaTipoMenu)
        {
            listaDetalles = listaDetalles
                .OrderBy(s => s.tipo_menu.orden)
                .ToList();
            //fin Listar Detalles elaboradas

            //contar valores de menus en cada unidad
            List<DUnidadMenuCantidades> listaUnidadesCantidades = new List<DUnidadMenuCantidades>();

            List<DRacionElaboradaDetalles> listaFiltroDetallesXUnidad = new List<DRacionElaboradaDetalles>();
            foreach (DUnidad unidad in listaUnidades)
            {
                // 🔴 NUEVA instancia en cada vuelta
                var unidadCantidades = new DUnidadMenuCantidades();
                //indicar la unidad enque se esta tomando los valores
                unidadCantidades.unidad = unidad.unidad;

                foreach (DTipoMenu tipoMenu in listaTipoMenu)
                {
                    listaFiltroDetallesXUnidad = listaDetalles.Where(x => x.unidad_id == unidad.id_unidad && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();
                    int almuerzo = 0;
                    int cena = 0;

                    foreach (DRacionElaboradaDetalles detalle in listaFiltroDetallesXUnidad)
                    {
                        almuerzo = almuerzo + detalle.almuerzo;
                        cena = cena + detalle.cena;
                    }

                    if (tipoMenu.id_tipo_menu == 1)
                    {
                        unidadCantidades.P12_A = almuerzo;
                        unidadCantidades.P12_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 2)
                    {
                        unidadCantidades.P24_A = almuerzo;
                        unidadCantidades.P24_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        unidadCantidades.IntN_A = almuerzo;
                        unidadCantidades.IntN_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 4)
                    {
                        unidadCantidades.Astr_A = almuerzo;
                        unidadCantidades.Astr_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 5)
                    {
                        unidadCantidades.Celi_A = almuerzo;
                        unidadCantidades.Celi_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 6)
                    {
                        unidadCantidades.AFib_A = almuerzo;
                        unidadCantidades.AFib_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 7)
                    {
                        unidadCantidades.Hep_A = almuerzo;
                        unidadCantidades.Hep_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 8)
                    {
                        unidadCantidades.SSal_A = almuerzo;
                        unidadCantidades.SSal_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 9)
                    {
                        unidadCantidades.HivTbc_A = almuerzo;
                        unidadCantidades.HivTbc_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 10)
                    {
                        unidadCantidades.Men_A = almuerzo;
                        unidadCantidades.Men_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 11)
                    {
                        unidadCantidades.SobreAl_A = almuerzo;
                        unidadCantidades.SobreAl_C = cena;
                    }


                }

                listaUnidadesCantidades.Add(unidadCantidades);
            }
            //fin contar valores de menus en cada unidad

            AgregarFilaTotales(listaUnidadesCantidades);

            return listaUnidadesCantidades;
        }
        //FIN METODO GENERAR LISTA UNIDAD CANTIDADES..................................

        //METODO GENERAR LISTA SAP CANTIDADES
        private static List<DSapMenuCantidades> GenerarListaSapCantidadesElaboradas(List<DRacionElaboradaDetalles> listaDetalles, List<DSap> listaSap, List<DTipoMenu> listaTipoMenu)
        {
            //ordenar Listar Detalles elaboradas
            listaDetalles = listaDetalles
                .OrderBy(s => s.tipo_menu.orden)
                .ToList();

            //contar valores de menus en cada sap
            List<DSapMenuCantidades> listaSapCantidades = new List<DSapMenuCantidades>();
            List<DRacionElaboradaDetalles> listaFiltroDetallesXSap = new List<DRacionElaboradaDetalles>();

            foreach (DSap sap in listaSap)
            {
                // 🔴 NUEVA instancia en cada vuelta
                var sapCantidades = new DSapMenuCantidades();
                sapCantidades.sap = sap.sap;

                foreach (DTipoMenu tipoMenu in listaTipoMenu)
                {
                    listaFiltroDetallesXSap = listaDetalles.Where(x => x.sap_id == sap.id_sap && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();

                    int almuerzo = 0;
                    int cena = 0;

                    foreach (DRacionElaboradaDetalles detalle in listaFiltroDetallesXSap)
                    {
                        almuerzo = almuerzo + detalle.almuerzo;
                        cena = cena + detalle.cena;
                    }

                    if (tipoMenu.id_tipo_menu == 1)
                    {
                        sapCantidades.P12_A = almuerzo;
                        sapCantidades.P12_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 2)
                    {
                        sapCantidades.P24_A = almuerzo;
                        sapCantidades.P24_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        sapCantidades.IntN_A = almuerzo;
                        sapCantidades.IntN_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 4)
                    {
                        sapCantidades.Astr_A = almuerzo;
                        sapCantidades.Astr_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 5)
                    {
                        sapCantidades.Celi_A = almuerzo;
                        sapCantidades.Celi_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 6)
                    {
                        sapCantidades.AFib_A = almuerzo;
                        sapCantidades.AFib_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 7)
                    {
                        sapCantidades.Hep_A = almuerzo;
                        sapCantidades.Hep_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 8)
                    {
                        sapCantidades.SSal_A = almuerzo;
                        sapCantidades.SSal_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 9)
                    {
                        sapCantidades.HivTbc_A = almuerzo;
                        sapCantidades.HivTbc_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 10)
                    {
                        sapCantidades.Men_A = almuerzo;
                        sapCantidades.Men_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 11)
                    {
                        sapCantidades.SobreAl_A = almuerzo;
                        sapCantidades.SobreAl_C = cena;
                    }

                }

                listaSapCantidades.Add(sapCantidades);
            }

            //fin contar valores de menus en cada sap

            AgregarFilaTotalesSap(listaSapCantidades);


            return listaSapCantidades;
        }
        //FIN METODO GENERAR LISTA SAP CANTIDADES ELABORADAS..................................

        //METODO GENERAR LISTA SAP ESTADISTICO
        private static List<DSapMenuEstadistico> GenerarListaSapEstadisticoElaboradas(List<DRacionElaborada> listaElaboradas, DSap sap, List<DTipoMenu> listaTipoMenu, DateTime fechaIni, DateTime fechaFin)
        {
            //ordenar Listar elaboradas
            listaElaboradas = listaElaboradas
                .OrderBy(s => s.fecha_elaborada)
                .ToList();

            //contar valores de menus en cada sap
            List<DSapMenuEstadistico> listaSapCantidades = new List<DSapMenuEstadistico>();
            List<DRacionElaborada> listaFiltroElaboradaXFecha = new List<DRacionElaborada>();
            List<DRacionElaboradaDetalles> listaFiltroDetallesXSap = new List<DRacionElaboradaDetalles>();

            for (DateTime fecha = fechaIni; fecha <= fechaFin; fecha = fecha.AddDays(1))
            {
                DRacionElaborada racionElaborada = listaElaboradas.Where(x => x.fecha_elaborada == fecha).First();
                List<DRacionElaboradaDetalles> listaDetallesElaboradas = racionElaborada.raciones_elaboradas_detalles.ToList();

                // 🔴 NUEVA instancia en cada vuelta
                var sapCantidades = new DSapMenuEstadistico();
                sapCantidades.fecha = fecha.ToShortDateString();

                foreach (DTipoMenu tipoMenu in listaTipoMenu)
                {
                    listaFiltroDetallesXSap = listaDetallesElaboradas.Where(x => x.sap_id == sap.id_sap && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();

                    int almuerzo = 0;
                    int cena = 0;

                    foreach (DRacionElaboradaDetalles detalle in listaFiltroDetallesXSap)
                    {
                        almuerzo = almuerzo + detalle.almuerzo;
                        cena = cena + detalle.cena;
                    }

                    if (tipoMenu.id_tipo_menu == 1)
                    {
                        sapCantidades.P12_A = almuerzo;
                        sapCantidades.P12_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 2)
                    {
                        sapCantidades.P24_A = almuerzo;
                        sapCantidades.P24_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        sapCantidades.IntN_A = almuerzo;
                        sapCantidades.IntN_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 4)
                    {
                        sapCantidades.Astr_A = almuerzo;
                        sapCantidades.Astr_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 5)
                    {
                        sapCantidades.Celi_A = almuerzo;
                        sapCantidades.Celi_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 6)
                    {
                        sapCantidades.AFib_A = almuerzo;
                        sapCantidades.AFib_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 7)
                    {
                        sapCantidades.Hep_A = almuerzo;
                        sapCantidades.Hep_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 8)
                    {
                        sapCantidades.SSal_A = almuerzo;
                        sapCantidades.SSal_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 9)
                    {
                        sapCantidades.HivTbc_A = almuerzo;
                        sapCantidades.HivTbc_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 10)
                    {
                        sapCantidades.Men_A = almuerzo;
                        sapCantidades.Men_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 11)
                    {
                        sapCantidades.SobreAl_A = almuerzo;
                        sapCantidades.SobreAl_C = cena;
                    }

                }

                listaSapCantidades.Add(sapCantidades);
            }

            //fin contar valores de menus en cada sap

            AgregarFilaTotalesSapEstadistico(listaSapCantidades);


            return listaSapCantidades;
        }
        //FIN METODO GENERAR LISTA SAP ESTADISTICO..................................


        //METODO GENERAR LISTA UNIDAD CANTIDADES SOLICITADAS
        private static List<DUnidadMenuCantidades> GenerarListaUnidadesCantidadesSolicitadas(List<DRacionesSolicitadasDetalles> listaDetalles, List<DUnidad> listaUnidades, List<DTipoMenu> listaTipoMenu)
        {
            listaDetalles = listaDetalles
                .OrderBy(s => s.tipo_menu.orden)
                .ToList();
            //fin Listar Detalles elaboradas

            //contar valores de menus en cada unidad
            List<DUnidadMenuCantidades> listaUnidadesCantidades = new List<DUnidadMenuCantidades>();

            List<DRacionesSolicitadasDetalles> listaFiltroDetallesXUnidad = new List<DRacionesSolicitadasDetalles>();
            foreach (DUnidad unidad in listaUnidades)
            {
                // 🔴 NUEVA instancia en cada vuelta
                var unidadCantidades = new DUnidadMenuCantidades();
                //indicar la unidad enque se esta tomando los valores
                unidadCantidades.unidad = unidad.unidad;

                foreach (DTipoMenu tipoMenu in listaTipoMenu)
                {
                    listaFiltroDetallesXUnidad = listaDetalles.Where(x => x.unidad_id == unidad.id_unidad && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();
                    int almuerzo = 0;
                    int cena = 0;

                    foreach (DRacionesSolicitadasDetalles detalle in listaFiltroDetallesXUnidad)
                    {
                        almuerzo = almuerzo + detalle.almuerzo;
                        cena = cena + detalle.cena;
                    }

                    if (tipoMenu.id_tipo_menu == 1)
                    {
                        unidadCantidades.P12_A = almuerzo;
                        unidadCantidades.P12_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 2)
                    {
                        unidadCantidades.P24_A = almuerzo;
                        unidadCantidades.P24_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        unidadCantidades.IntN_A = almuerzo;
                        unidadCantidades.IntN_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 4)
                    {
                        unidadCantidades.Astr_A = almuerzo;
                        unidadCantidades.Astr_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 5)
                    {
                        unidadCantidades.Celi_A = almuerzo;
                        unidadCantidades.Celi_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 6)
                    {
                        unidadCantidades.AFib_A = almuerzo;
                        unidadCantidades.AFib_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 7)
                    {
                        unidadCantidades.Hep_A = almuerzo;
                        unidadCantidades.Hep_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 8)
                    {
                        unidadCantidades.SSal_A = almuerzo;
                        unidadCantidades.SSal_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 9)
                    {
                        unidadCantidades.HivTbc_A = almuerzo;
                        unidadCantidades.HivTbc_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 10)
                    {
                        unidadCantidades.Men_A = almuerzo;
                        unidadCantidades.Men_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 11)
                    {
                        unidadCantidades.SobreAl_A = almuerzo;
                        unidadCantidades.SobreAl_C = cena;
                    }


                }

                listaUnidadesCantidades.Add(unidadCantidades);
            }
            //fin contar valores de menus en cada unidad

            AgregarFilaTotales(listaUnidadesCantidades);

            return listaUnidadesCantidades;
        }
        //FIN METODO GENERAR LISTA UNIDAD CANTIDADES SOLICITADAS..................................

        //METODO GENERAR LISTA SAP CANTIDADES SOLICITADAS
        private static List<DSapMenuCantidades> GenerarListaSapCantidadesSolicitadas(List<DRacionesSolicitadasDetalles> listaDetalles, List<DSap> listaSap, List<DTipoMenu> listaTipoMenu)
        {
            //ordenar Listar Detalles elaboradas
            listaDetalles = listaDetalles
                .OrderBy(s => s.tipo_menu.orden)
                .ToList();

            //contar valores de menus en cada sap
            List<DSapMenuCantidades> listaSapCantidades = new List<DSapMenuCantidades>();
            List<DRacionesSolicitadasDetalles> listaFiltroDetallesXSap = new List<DRacionesSolicitadasDetalles>();

            foreach (DSap sap in listaSap)
            {
                // 🔴 NUEVA instancia en cada vuelta
                var sapCantidades = new DSapMenuCantidades();
                sapCantidades.sap = sap.sap;

                foreach (DTipoMenu tipoMenu in listaTipoMenu)
                {
                    listaFiltroDetallesXSap = listaDetalles.Where(x => x.sap_id == sap.id_sap && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();

                    int almuerzo = 0;
                    int cena = 0;

                    foreach (DRacionesSolicitadasDetalles detalle in listaFiltroDetallesXSap)
                    {
                        almuerzo = almuerzo + detalle.almuerzo;
                        cena = cena + detalle.cena;
                    }

                    if (tipoMenu.id_tipo_menu == 1)
                    {
                        sapCantidades.P12_A = almuerzo;
                        sapCantidades.P12_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 2)
                    {
                        sapCantidades.P24_A = almuerzo;
                        sapCantidades.P24_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        sapCantidades.IntN_A = almuerzo;
                        sapCantidades.IntN_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 4)
                    {
                        sapCantidades.Astr_A = almuerzo;
                        sapCantidades.Astr_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 5)
                    {
                        sapCantidades.Celi_A = almuerzo;
                        sapCantidades.Celi_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 6)
                    {
                        sapCantidades.AFib_A = almuerzo;
                        sapCantidades.AFib_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 7)
                    {
                        sapCantidades.Hep_A = almuerzo;
                        sapCantidades.Hep_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 8)
                    {
                        sapCantidades.SSal_A = almuerzo;
                        sapCantidades.SSal_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 9)
                    {
                        sapCantidades.HivTbc_A = almuerzo;
                        sapCantidades.HivTbc_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 10)
                    {
                        sapCantidades.Men_A = almuerzo;
                        sapCantidades.Men_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 11)
                    {
                        sapCantidades.SobreAl_A = almuerzo;
                        sapCantidades.SobreAl_C = cena;
                    }

                }

                listaSapCantidades.Add(sapCantidades);
            }

            //fin contar valores de menus en cada sap

            AgregarFilaTotalesSap(listaSapCantidades);


            return listaSapCantidades;
        }
        //FIN METODO GENERAR LISTA SAP CANTIDADES SOLICITADAS..................................


        //METODO GENERAR LISTA UNIDADES PARTE NOVEDADES
        private static List<DUnidadMenuCantidades> GenerarListaCantidadesParteNovedadesDiario(List<DRacionElaborada> listaElaboradas, List<DUnidad> listaUnidades, List<DTipoMenu> listaTipoMenu, DateTime fechaElaborada)
        {
            //ordenar Listar elaboradas
            listaElaboradas = listaElaboradas
                .OrderBy(s => s.fecha_elaborada)
                .ToList();

            //contar valores de menus en cada sap
            List<DUnidadMenuCantidades> listaUnidadCantidades = new List<DUnidadMenuCantidades>();
            List<DRacionElaborada> listaFiltroElaboradaXFecha = new List<DRacionElaborada>();
            List<DRacionElaboradaDetalles> listaFiltroDetallesXUnidad= new List<DRacionElaboradaDetalles>();

            DRacionElaborada racionElaborada = listaElaboradas.Where(x => x.fecha_elaborada == fechaElaborada).First();
            List<DRacionElaboradaDetalles> listaDetallesElaboradas = racionElaborada.raciones_elaboradas_detalles.ToList();

            //for (DateTime fecha = fechaIni; fecha <= fechaFin; fecha = fecha.AddDays(1))
            foreach (DUnidad unidad in listaUnidades)
            {
                // 🔴 NUEVA instancia en cada vuelta
                var unidadCantidades = new DUnidadMenuCantidades();
                unidadCantidades.unidad = unidad.unidad;

                foreach (DTipoMenu tipoMenu in listaTipoMenu)
                {
                    listaFiltroDetallesXUnidad = listaDetallesElaboradas.Where(x => x.unidad_id == unidad.id_unidad && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();

                    int almuerzo = 0;
                    int cena = 0;

                    foreach (DRacionElaboradaDetalles detalle in listaFiltroDetallesXUnidad)
                    {
                        almuerzo = almuerzo + detalle.almuerzo;
                        cena = cena + detalle.cena;
                    }

                    if (tipoMenu.id_tipo_menu == 1)
                    {
                        unidadCantidades.P12_A = almuerzo;
                        unidadCantidades.P12_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 2)
                    {
                        unidadCantidades.P24_A = almuerzo;
                        unidadCantidades.P24_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        unidadCantidades.IntN_A = almuerzo;
                        unidadCantidades.IntN_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 4)
                    {
                        unidadCantidades.Astr_A = almuerzo;
                        unidadCantidades.Astr_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 5)
                    {
                        unidadCantidades.Celi_A = almuerzo;
                        unidadCantidades.Celi_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 6)
                    {
                        unidadCantidades.AFib_A = almuerzo;
                        unidadCantidades.AFib_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 7)
                    {
                        unidadCantidades.Hep_A = almuerzo;
                        unidadCantidades.Hep_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 8)
                    {
                        unidadCantidades.SSal_A = almuerzo;
                        unidadCantidades.SSal_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 9)
                    {
                        unidadCantidades.HivTbc_A = almuerzo;
                        unidadCantidades.HivTbc_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 10)
                    {
                        unidadCantidades.Men_A = almuerzo;
                        unidadCantidades.Men_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 11)
                    {
                        unidadCantidades.SobreAl_A = almuerzo;
                        unidadCantidades.SobreAl_C = cena;
                    }

                }

                listaUnidadCantidades.Add(unidadCantidades);
            }

            //fin contar valores de menus en cada sap

            AgregarFilaTotalesUnidadParteNovedades(listaUnidadCantidades);


            return listaUnidadCantidades;
        }
        //FIN METODO GENERAR LISTA UNIDADES PARTE NOVEDADES..................................


        //METODO AGREGAR PAGINA OBSERVACIONES GENERALES PARTE DIARIO
        private static void AgregarPaginaObsGeneralesParteDiario(Document doc, List<DObservacionGeneral> listaObservacionesGenerales, DateTime fechaInicio, DateTime fechaFin)
        {
            // Fuentes
            Font fuenteLogo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteTitulo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 11);            
            Font fuenteObservaciones = FontFactory.GetFont(FontFactory.TIMES, 10);

            // Encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.WidthPercentage = 100;
            tablaEncabezado.SetWidths(new float[] { 30f, 70f });

            PdfPCell celdaIzq = new PdfPCell(new Phrase("SERVICIO PENITENCIARIO DE LA\nPROVINCIA DE SALTA\nDiv. Nutrición", fuenteLogo));

            celdaIzq.Border = Rectangle.NO_BORDER;
            celdaIzq.HorizontalAlignment = Element.ALIGN_CENTER;

            tablaEncabezado.AddCell(celdaIzq);

            doc.Add(tablaEncabezado);

            Paragraph titulo = new Paragraph("Observaciones generales de Rendicion desde " + fechaInicio.ToString("dddd d 'de' MMMM 'de' yyyy") + " hasta " + fechaFin.ToString("dddd d 'de' MMMM 'de' yyyy"), fuenteTitulo);

            titulo.Alignment = Element.ALIGN_CENTER;

            doc.Add(titulo);

            doc.Add(new Paragraph(" "));

            //OBSERVACIONES
            Font fuenteNegrita = new Font(fuenteObservaciones.BaseFont, fuenteObservaciones.Size, Font.BOLD, fuenteObservaciones.Color);
            Font fuenteNormal = fuenteObservaciones; // Mantiene tu fuente original para el texto

            // 2. Crear un único párrafo contenedor
            Paragraph parrafoContenedor = new Paragraph();

            foreach (DObservacionGeneral observacion in listaObservacionesGenerales)
            {
                // 3. Si no es la primera observación, agregar un salto de línea físico antes de la nueva
                if (parrafoContenedor.Chunks.Count > 0)
                {
                    parrafoContenedor.Add(new Chunk("\n", fuenteNormal));
                }

                // 4. Agregar "Obs: " con la fuente en negrita (se queda en la misma línea)
                parrafoContenedor.Add(new Chunk("Obs: ", fuenteNegrita));

                // 5. Agregar el texto de la observación con la fuente normal (en la misma línea)
                parrafoContenedor.Add(new Chunk(observacion.observacion.ToString(), fuenteNormal));
            }

            // 6. Agregar el párrafo completo al documento de una sola vez
            doc.Add(parrafoContenedor);
            
        }
        //FIN METODO AGREGAR PAGINA OBSERVACIONES GENERALES...................................................................

        //METODO AGREGAR PAGINA - tipo_planilla = SOLICITADAS o ELABORADAS
        private static void AgregarPagina(Document doc, List<DUnidadMenuCantidades> listaUnidadesCantidades, List<DSapMenuCantidades> listaSapsCantidades, List<DObservacionElaborada> listaObservacionesElaboradas, List<DObservacionSolicitada> listaObservacionesSolicitadas, DateTime fecha, string tipo_planilla, string titulo_tabla)
        {

            // Fuentes
            Font fuenteLogo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteTitulo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteTituloTabla = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
            Font fuenteEncabezado = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteCelda = FontFactory.GetFont(FontFactory.TIMES, 9);
            Font fuenteTotales = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteObservaciones = FontFactory.GetFont(FontFactory.TIMES, 10);

            // Encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.WidthPercentage = 100;
            tablaEncabezado.SetWidths(new float[] { 30f, 70f });

            PdfPCell celdaIzq = new PdfPCell(new Phrase("SERVICIO PENITENCIARIO DE LA\nPROVINCIA DE SALTA\nDiv. Nutrición", fuenteLogo));

            celdaIzq.Border = Rectangle.NO_BORDER;
            celdaIzq.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell celdaFecha = new PdfPCell(new Phrase("Salta, " + fecha.ToString("dddd d 'de' MMMM 'de' yyyy"), fuenteLogo));

            celdaFecha.Border = Rectangle.NO_BORDER;
            celdaFecha.HorizontalAlignment = Element.ALIGN_RIGHT;
            celdaFecha.VerticalAlignment = Element.ALIGN_BOTTOM;

            tablaEncabezado.AddCell(celdaIzq);
            tablaEncabezado.AddCell(celdaFecha);

            doc.Add(tablaEncabezado);

            Paragraph titulo = new Paragraph("Raciones " + tipo_planilla, fuenteTitulo);

            titulo.Alignment = Element.ALIGN_CENTER;

            doc.Add(titulo);

            //doc.Add(new Paragraph(" "));

            // AQUÍ VENDRÁ LA TABLA
            PdfPTable tabla = new PdfPTable(23);
            tabla.WidthPercentage = 100;

            tabla.SetWidths(new float[]
            {
                3f,      // Unidad

                .7f,.7f, // P12
                .7f,.7f, // P24
                .7f,.7f, // IntN
                .7f,.7f, // Astr
                .7f,.7f, // Celi
                .7f,.7f, // AFib
                .7f,.7f, // Hep
                .7f,.7f, // SSal
                .7f,.7f, // HivTbc
                .7f,.7f, // Men
                .7f,.7f  // SobreAl
            });

            //agregar filas de encabezados
            // UNIDADES
            PdfPCell celdaUnidad = new PdfPCell(new Phrase(titulo_tabla, fuenteTituloTabla));
            celdaUnidad.Rowspan = 3;
            celdaUnidad.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaUnidad.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(celdaUnidad);

            // PERSONAL (12Hs + 24Hs = 4 columnas)
            PdfPCell celdaPersonal = new PdfPCell(new Phrase("PERSONAL", fuenteEncabezado));

            celdaPersonal.Colspan = 4;
            celdaPersonal.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(celdaPersonal);

            // INTERNOS NORMAL (2 columnas)
            PdfPCell celdaInternos = new PdfPCell(new Phrase("Internos\n(Normal)", fuenteEncabezado));
            celdaInternos.Colspan = 2;
            celdaInternos.Rowspan = 2;
            celdaInternos.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaInternos.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(celdaInternos);

            // REGIMEN DIETOTERAPICO (16 columnas)
            PdfPCell celdaRegimen = new PdfPCell(new Phrase("Régimen DIETOTERÁPICO: Personal/Internos", fuenteEncabezado));
            celdaRegimen.Colspan = 16;
            celdaRegimen.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(celdaRegimen);

            //--segunda fila encabezado
            AgregarGrupo(tabla, "12Hs", fuenteEncabezado);
            AgregarGrupo(tabla, "24Hs", fuenteEncabezado);

            AgregarGrupo(tabla, "Dieta\nAstring.", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta\nCelíaco", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta Alta\nen Fibra", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta Hepato\nProtectora", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta\nS/Sal", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta\nHIV/TBC", fuenteEncabezado);
            AgregarGrupo(tabla, "Menores", fuenteEncabezado);
            AgregarGrupo(tabla, "Sobre\nAlim.", fuenteEncabezado);


            //--tercer fila encabezado

            BaseColor colorBlanco = BaseColor.WHITE;

            //color para celdas de solicitadas
            BaseColor colorAlternado = new BaseColor(244, 220, 180); // parecido a SandyBrown
            //color para celdas de elaboradas
            if (tipo_planilla == "ELABORADAS")
            {
                colorAlternado = new BaseColor(230, 240, 255); // azul muy suave

            }

            for (int i = 0; i < 11; i++)
            {
                BaseColor color = (i % 2 == 0)
                    ? colorBlanco
                    : colorAlternado;

                PdfPCell celdaA = new PdfPCell(new Phrase("Alm.", fuenteEncabezado));
                celdaA.BackgroundColor = color;
                celdaA.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(celdaA);

                PdfPCell celdaC = new PdfPCell(new Phrase("Cena", fuenteEncabezado));
                celdaC.BackgroundColor = color;
                celdaC.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(celdaC);
            }

            //agregar celdas: usar listaUnidadesCantidades para agregar celdas con valores
            if (listaSapsCantidades == null)
            {
                //agregar celdas
                foreach (DUnidadMenuCantidades item in listaUnidadesCantidades)
                {
                    Font fuente = item.unidad == "Totales"
                        ? fuenteTotales
                        : fuenteCelda;

                    BaseColor colorFondo = item.unidad == "Totales"
                        ? BaseColor.LIGHT_GRAY
                        : BaseColor.WHITE;


                    AgregarCelda(tabla, item.unidad, fuente, item.unidad == "Totales" ? BaseColor.LIGHT_GRAY : BaseColor.WHITE,
                        Element.ALIGN_LEFT);

                    // Valores numéricos
                    int[] valores =
                    {
                    item.P12_A, item.P12_C,
                    item.P24_A, item.P24_C,
                    item.IntN_A, item.IntN_C,
                    item.Astr_A, item.Astr_C,
                    item.Celi_A, item.Celi_C,
                    item.AFib_A, item.AFib_C,
                    item.Hep_A, item.Hep_C,
                    item.SSal_A, item.SSal_C,
                    item.HivTbc_A, item.HivTbc_C,
                    item.Men_A, item.Men_C,
                    item.SobreAl_A, item.SobreAl_C
                };

                    //color de las columnas
                    BaseColor colorAlternado2 = new BaseColor(244, 220, 180);

                    //color para celdas de elaboradas
                    if (tipo_planilla == "ELABORADAS")
                    {
                        colorAlternado2 = new BaseColor(230, 240, 255); // azul muy suave

                    }

                    for (int grupo = 0; grupo < 11; grupo++)
                    {
                        BaseColor colorGrupo = (grupo % 2 == 0)
                            ? BaseColor.WHITE
                            : colorAlternado2;

                        // Si es la fila Totales, mantener gris
                        if (item.unidad == "Totales")
                            colorGrupo = BaseColor.LIGHT_GRAY;

                        AgregarCelda(tabla, valores[grupo * 2].ToString(), fuente, colorGrupo);

                        AgregarCelda(tabla, valores[grupo * 2 + 1].ToString(), fuente, colorGrupo);
                    }
                }
            }

            //agregar celdas: usar listaSapsCantidades para agregar celdas con valores
            if (listaUnidadesCantidades == null)
            {
                //agregar celdas
                foreach (DSapMenuCantidades item in listaSapsCantidades)
                {
                    Font fuente = item.sap == "Totales"
                        ? fuenteTotales
                        : fuenteCelda;

                    BaseColor colorFondo = item.sap == "Totales"
                        ? BaseColor.LIGHT_GRAY
                        : BaseColor.WHITE;


                    AgregarCelda(tabla, item.sap, fuente, item.sap == "Totales" ? BaseColor.LIGHT_GRAY : BaseColor.WHITE,
                        Element.ALIGN_LEFT);

                    // Valores numéricos
                    int[] valores =
                    {
                        item.P12_A, item.P12_C,
                        item.P24_A, item.P24_C,
                        item.IntN_A, item.IntN_C,
                        item.Astr_A, item.Astr_C,
                        item.Celi_A, item.Celi_C,
                        item.AFib_A, item.AFib_C,
                        item.Hep_A, item.Hep_C,
                        item.SSal_A, item.SSal_C,
                        item.HivTbc_A, item.HivTbc_C,
                        item.Men_A, item.Men_C,
                        item.SobreAl_A, item.SobreAl_C
                    };

                    //color de las columnas
                    BaseColor colorAlternado2 = new BaseColor(244, 220, 180);

                    //color para celdas de elaboradas
                    if (tipo_planilla == "ELABORADAS")
                    {
                        colorAlternado2 = new BaseColor(230, 240, 255); // azul muy suave

                    }

                    for (int grupo = 0; grupo < 11; grupo++)
                    {
                        BaseColor colorGrupo = (grupo % 2 == 0)
                            ? BaseColor.WHITE
                            : colorAlternado2;

                        // Si es la fila Totales, mantener gris
                        if (item.sap == "Totales")
                            colorGrupo = BaseColor.LIGHT_GRAY;

                        AgregarCelda(tabla, valores[grupo * 2].ToString(), fuente, colorGrupo);

                        AgregarCelda(tabla, valores[grupo * 2 + 1].ToString(), fuente, colorGrupo);
                    }
                }
            }

            //agregar tabla
            doc.Add(tabla);

            //agregar observaciones: luego de tabla sap solicitadas
            if (listaUnidadesCantidades == null && tipo_planilla == "SOLICITADAS")
            {
                //string todasObservaciones = "";
                //foreach (DObservacionSolicitada observacion in listaObservacionesSolicitadas)
                //{
                //    todasObservaciones = todasObservaciones + "\nObs: " + observacion.observacion.ToString();

                //}
                //doc.Add(new Paragraph(todasObservaciones, fuenteObservaciones));

                // 1. Crear las dos fuentes que necesitas (basadas en tu fuente original)
                // Cambia 'fuenteObservaciones.Size' si tu variable tiene otro nombre para el tamaño
                Font fuenteNegrita = new Font(fuenteObservaciones.BaseFont, fuenteObservaciones.Size, Font.BOLD, fuenteObservaciones.Color);
                Font fuenteNormal = fuenteObservaciones; // Mantiene tu fuente original para el texto

                // 2. Crear un único párrafo contenedor
                Paragraph parrafoContenedor = new Paragraph();

                foreach (DObservacionSolicitada observacion in listaObservacionesSolicitadas)
                {
                    // 3. Si no es la primera observación, agregar un salto de línea físico antes de la nueva
                    if (parrafoContenedor.Chunks.Count > 0)
                    {
                        parrafoContenedor.Add(new Chunk("\n", fuenteNormal));
                    }

                    // 4. Agregar "Obs: " con la fuente en negrita (se queda en la misma línea)
                    parrafoContenedor.Add(new Chunk("Obs: ", fuenteNegrita));

                    // 5. Agregar el texto de la observación con la fuente normal (en la misma línea)
                    parrafoContenedor.Add(new Chunk(observacion.observacion.ToString(), fuenteNormal));
                }

                // 6. Agregar el párrafo completo al documento de una sola vez
                doc.Add(parrafoContenedor);
            }

            //agregar observaciones: luego de tabla sap elaboradas
            if (listaUnidadesCantidades == null && tipo_planilla == "ELABORADAS")
            {
                //string todasObservaciones = "";
                //foreach(DObservacionElaborada observacion in listaObservacionesElaboradas)
                //{
                //    todasObservaciones = todasObservaciones + "\nObs: " + observacion.observacion.ToString();

                //}
                //doc.Add(new Paragraph("Obs: " + todasObservaciones, fuenteObservaciones));

                // 1. Crear las dos fuentes que necesitas (basadas en tu fuente original)
                // Cambia 'fuenteObservaciones.Size' si tu variable tiene otro nombre para el tamaño
                Font fuenteNegrita = new Font(fuenteObservaciones.BaseFont, fuenteObservaciones.Size, Font.BOLD, fuenteObservaciones.Color);
                Font fuenteNormal = fuenteObservaciones; // Mantiene tu fuente original para el texto

                // 2. Crear un único párrafo contenedor
                Paragraph parrafoContenedor = new Paragraph();

                foreach (DObservacionElaborada observacion in listaObservacionesElaboradas)
                {
                    // 3. Si no es la primera observación, agregar un salto de línea físico antes de la nueva
                    if (parrafoContenedor.Chunks.Count > 0)
                    {
                        parrafoContenedor.Add(new Chunk("\n", fuenteNormal));
                    }

                    // 4. Agregar "Obs: " con la fuente en negrita (se queda en la misma línea)
                    parrafoContenedor.Add(new Chunk("Obs: ", fuenteNegrita));

                    // 5. Agregar el texto de la observación con la fuente normal (en la misma línea)
                    parrafoContenedor.Add(new Chunk(observacion.observacion.ToString(), fuenteNormal));
                }

                // 6. Agregar el párrafo completo al documento de una sola vez
                doc.Add(parrafoContenedor);
            }
        }

        //FIN METODO AGREGAR PAGINA...................................................................



        //METODO AGREGAR PAGINA - tipo_planilla = SOLICITADAS o ELABORADAS
        private static void AgregarPaginaSapCantidades(Document doc, PdfWriter writer,  List<DRacionElaboradaDetalles> listaElaboradasDetalles, DateTime fecha, List<DSap> listaSap, List<DTipoMenu> listaTipoMenu)
        {

            // Fuentes
            Font fuenteLogo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteTitulo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteTituloTabla = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
            Font fuenteEncabezado = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteCelda = FontFactory.GetFont(FontFactory.TIMES, 9);
            Font fuenteTotales = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);

            // Encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.WidthPercentage = 100;
            tablaEncabezado.SetWidths(new float[] { 30f, 70f });

            PdfPCell celdaIzq = new PdfPCell(new Phrase("SERVICIO PENITENCIARIO DE LA\nPROVINCIA DE SALTA\nDiv. Nutrición", fuenteLogo));

            celdaIzq.Border = Rectangle.NO_BORDER;
            celdaIzq.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell celdaFecha = new PdfPCell(new Phrase("Salta, " + fecha.ToString("dddd d 'de' MMMM 'de' yyyy"), fuenteLogo));

            celdaFecha.Border = Rectangle.NO_BORDER;
            celdaFecha.HorizontalAlignment = Element.ALIGN_RIGHT;
            celdaFecha.VerticalAlignment = Element.ALIGN_BOTTOM;

            tablaEncabezado.AddCell(celdaIzq);
            tablaEncabezado.AddCell(celdaFecha);

            doc.Add(tablaEncabezado);
                   
            doc.Add(new Paragraph(" "));

            // AQUÍ VENDRÁ LA TABLA
            PdfPTable tabla = new PdfPTable(15);
            tabla.WidthPercentage = 100;

            tabla.SetWidths(new float[]
            {
                2f,      // Unidad

                .7f,.7f, // P12
                .7f,.7f, // P24
                .7f,.7f, // IntN
                .7f,.7f, // Astr
                .7f,.7f, // Celi
                .7f,.7f, // AFib
                .7f,.7f, // Hep
                
            });

            //ENCABEZADO tabla
            PdfPCell celda = new PdfPCell(new Phrase("Menus Elaborados", fuenteEncabezado));
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(celda);

            BaseColor colorBlanco = BaseColor.WHITE;
            BaseColor colorAlternado = new BaseColor(230, 240, 255); // azul muy suave

            // Columnas dinámicas por SAP
            for (int num_sap = 1; num_sap <= 14; num_sap++)
            {
                BaseColor colorCelda = (num_sap % 2 == 0)
                       ? BaseColor.WHITE
                       : colorAlternado;

                PdfPCell celdaA = new PdfPCell(new Phrase("SAP Nº " + num_sap, fuenteEncabezado));
                celdaA.BackgroundColor = colorCelda;
                celdaA.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(celdaA);
            }
            //FIN Encabezado tabla

            //FILAS de tabla
            List<DRacionElaboradaDetalles> listaFiltroDetallesXMenuSap = new List<DRacionElaboradaDetalles>();
            List<DTipoMenu> listaTipoMenusFiltro = listaTipoMenu.Where(x => x.id_tipo_menu != 11 && x.id_tipo_menu != 10).ToList();

            foreach (var tipoMenu in listaTipoMenusFiltro)
            {

                //desayuno
                PdfPCell celdaMenuDsayuno = new PdfPCell(new Phrase(tipoMenu.tipo_menu + " - Desayuno", fuenteEncabezado));
                //celdaA.BackgroundColor = color;
                celdaMenuDsayuno.HorizontalAlignment = Element.ALIGN_LEFT;
                tabla.AddCell(celdaMenuDsayuno);

                int numero_columna = 0;

                numero_columna = 0;
                foreach (var sap in listaSap)
                {
                    if(tipoMenu.id_tipo_menu == 3)
                    {
                        listaFiltroDetallesXMenuSap = listaElaboradasDetalles.Where(x => x.sap_id == sap.id_sap && (x.tipo_menu_id == tipoMenu.id_tipo_menu || x.tipo_menu_id == 10)).ToList();

                    }
                    else
                    {
                        listaFiltroDetallesXMenuSap = listaElaboradasDetalles.Where(x => x.sap_id == sap.id_sap && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();

                    }

                    int desayuno = 0;

                    foreach (DRacionElaboradaDetalles detalle in listaFiltroDetallesXMenuSap)
                    {
                        desayuno = desayuno + detalle.almuerzo;
                    }

                    numero_columna = numero_columna + 1;
                    BaseColor colorCeldaCantidad = (numero_columna % 2 == 0)
                       ? BaseColor.WHITE
                       : colorAlternado;

                    PdfPCell celdaDesayuno = new PdfPCell(new Phrase(desayuno.ToString(), fuenteCelda));
                    celdaDesayuno.BackgroundColor = colorCeldaCantidad;
                    celdaDesayuno.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celdaDesayuno);
                       
                }

                //almuerzo
                PdfPCell celdaMenuAlmuerzo = new PdfPCell(new Phrase(tipoMenu.tipo_menu + " - Almuerzo", fuenteEncabezado));
                //celdaA.BackgroundColor = color;
                celdaMenuAlmuerzo.HorizontalAlignment = Element.ALIGN_LEFT;
                tabla.AddCell(celdaMenuAlmuerzo);

                numero_columna = 0;
                foreach (var sap in listaSap)
                {
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        listaFiltroDetallesXMenuSap = listaElaboradasDetalles.Where(x => x.sap_id == sap.id_sap && (x.tipo_menu_id == tipoMenu.id_tipo_menu || x.tipo_menu_id == 10)).ToList();

                    }
                    else
                    {
                        listaFiltroDetallesXMenuSap = listaElaboradasDetalles.Where(x => x.sap_id == sap.id_sap && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();

                    }

                    int almuerzo = 0;

                    foreach (DRacionElaboradaDetalles detalle in listaFiltroDetallesXMenuSap)
                    {
                        almuerzo = almuerzo + detalle.almuerzo;
                    }

                    numero_columna = numero_columna + 1;
                    BaseColor colorCeldaCantidad = (numero_columna % 2 == 0)
                       ? BaseColor.WHITE
                       : colorAlternado;
                    PdfPCell celdaAlmuerzo = new PdfPCell(new Phrase(almuerzo.ToString(), fuenteCelda));
                    celdaAlmuerzo.BackgroundColor = colorCeldaCantidad;
                    celdaAlmuerzo.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celdaAlmuerzo);

                }

                //merienda
                PdfPCell celdaMenuMerienda = new PdfPCell(new Phrase(tipoMenu.tipo_menu + " - Merienda", fuenteEncabezado));
                //celdaA.BackgroundColor = color;
                celdaMenuMerienda.HorizontalAlignment = Element.ALIGN_LEFT;
                tabla.AddCell(celdaMenuMerienda);

                numero_columna = 0;
                foreach (var sap in listaSap)
                {
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        listaFiltroDetallesXMenuSap = listaElaboradasDetalles.Where(x => x.sap_id == sap.id_sap && (x.tipo_menu_id == tipoMenu.id_tipo_menu || x.tipo_menu_id == 10)).ToList();

                    }
                    else
                    {
                        listaFiltroDetallesXMenuSap = listaElaboradasDetalles.Where(x => x.sap_id == sap.id_sap && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();

                    }

                    int merienda = 0;

                    foreach (DRacionElaboradaDetalles detalle in listaFiltroDetallesXMenuSap)
                    {
                        merienda = merienda + detalle.cena;
                    }

                    numero_columna = numero_columna + 1;
                    BaseColor colorCeldaCantidad = (numero_columna % 2 == 0)
                       ? BaseColor.WHITE
                       : colorAlternado;
                    PdfPCell celdaMerienda = new PdfPCell(new Phrase(merienda.ToString(), fuenteCelda));
                    celdaMerienda.BackgroundColor = colorCeldaCantidad;
                    celdaMerienda.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celdaMerienda);

                }

                //Cena
                PdfPCell celdaMenuCena = new PdfPCell(new Phrase(tipoMenu.tipo_menu + " - Cena", fuenteEncabezado));
                //celdaA.BackgroundColor = color;
                celdaMenuCena.HorizontalAlignment = Element.ALIGN_LEFT;
                tabla.AddCell(celdaMenuCena);

                numero_columna = 0;
                foreach (var sap in listaSap)
                {
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        listaFiltroDetallesXMenuSap = listaElaboradasDetalles.Where(x => x.sap_id == sap.id_sap && (x.tipo_menu_id == tipoMenu.id_tipo_menu || x.tipo_menu_id == 10)).ToList();

                    }
                    else
                    {
                        listaFiltroDetallesXMenuSap = listaElaboradasDetalles.Where(x => x.sap_id == sap.id_sap && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();

                    }

                    int cena = 0;

                    foreach (DRacionElaboradaDetalles detalle in listaFiltroDetallesXMenuSap)
                    {
                        cena = cena + detalle.cena;
                    }

                    numero_columna = numero_columna + 1;
                    BaseColor colorCeldaCantidad = (numero_columna % 2 == 0)
                       ? BaseColor.WHITE
                       : colorAlternado;
                    PdfPCell celdaCena = new PdfPCell(new Phrase(cena.ToString(), fuenteCelda));
                    celdaCena.BackgroundColor = colorCeldaCantidad;
                    celdaCena.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celdaCena);

                }

            }
            //FIN FILAS de tabla

            //agregar tabla
            doc.Add(tabla);

            //tabla firmas
            AgregarFirmasPiePagina(doc, writer);

        }

        //FIN METODO AGREGAR PAGINA...................................................................

        //METODO AGREGAR PAGINA - tipo_planilla = SOLICITADAS o ELABORADAS
        private static void AgregarPaginaEstadistico(Document doc, List<DSapMenuEstadistico> listaSapsCantidades, DSap sap, string tipo_planilla)
        {

            // Fuentes
            Font fuenteLogo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteTitulo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteTituloTabla = FontFactory.GetFont(FontFactory.TIMES_BOLD, 11);
            Font fuenteEncabezado = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteCelda = FontFactory.GetFont(FontFactory.TIMES, 9);
            Font fuenteTotales = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);

            // Encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.WidthPercentage = 100;
            tablaEncabezado.SetWidths(new float[] { 30f, 70f });

            PdfPCell celdaIzq = new PdfPCell(new Phrase("SERVICIO PENITENCIARIO DE LA\nPROVINCIA DE SALTA\nDiv. Nutrición", fuenteLogo));

            celdaIzq.Border = Rectangle.NO_BORDER;
            celdaIzq.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaEncabezado.AddCell(celdaIzq);

            doc.Add(tablaEncabezado);

            Paragraph titulo = new Paragraph("Estadistico " + tipo_planilla, fuenteTitulo);

            titulo.Alignment = Element.ALIGN_CENTER;

            doc.Add(titulo);

            doc.Add(new Paragraph(" "));

            // AQUÍ VENDRÁ LA TABLA
            PdfPTable tabla = new PdfPTable(23);
            tabla.WidthPercentage = 100;

            tabla.SetWidths(new float[]
            {
                3f,      // Unidad

                .7f,.7f, // P12
                .7f,.7f, // P24
                .7f,.7f, // IntN
                .7f,.7f, // Astr
                .7f,.7f, // Celi
                .7f,.7f, // AFib
                .7f,.7f, // Hep
                .7f,.7f, // SSal
                .7f,.7f, // HivTbc
                .7f,.7f, // Men
                .7f,.7f  // SobreAl
            });

            //agregar filas de encabezados
            // UNIDADES
            string titulo_tabla = sap.sap;
            PdfPCell celdaSap = new PdfPCell(new Phrase(titulo_tabla, fuenteTituloTabla));
            celdaSap.Rowspan = 3;
            celdaSap.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaSap.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(celdaSap);

            // PERSONAL (12Hs + 24Hs = 4 columnas)
            PdfPCell celdaPersonal = new PdfPCell(new Phrase("PERSONAL", fuenteEncabezado));

            celdaPersonal.Colspan = 4;
            celdaPersonal.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(celdaPersonal);

            // INTERNOS NORMAL (2 columnas)
            PdfPCell celdaInternos = new PdfPCell(new Phrase("Internos\n(Normal)", fuenteEncabezado));
            celdaInternos.Colspan = 2;
            celdaInternos.Rowspan = 2;
            celdaInternos.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaInternos.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(celdaInternos);

            // REGIMEN DIETOTERAPICO (16 columnas)
            PdfPCell celdaRegimen = new PdfPCell(new Phrase("Régimen DIETOTERÁPICO: Personal/Internos", fuenteEncabezado));
            celdaRegimen.Colspan = 16;
            celdaRegimen.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(celdaRegimen);

            //--segunda fila encabezado
            AgregarGrupo(tabla, "12Hs", fuenteEncabezado);
            AgregarGrupo(tabla, "24Hs", fuenteEncabezado);

            AgregarGrupo(tabla, "Dieta\nAstring.", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta\nCelíaco", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta Alta\nen Fibra", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta Hepato\nProtectora", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta\nS/Sal", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta\nHIV/TBC", fuenteEncabezado);
            AgregarGrupo(tabla, "Menores", fuenteEncabezado);
            AgregarGrupo(tabla, "Sobre\nAlim.", fuenteEncabezado);


            //--tercer fila encabezado

            BaseColor colorBlanco = BaseColor.WHITE;

            //color para celdas de solicitadas
            BaseColor colorAlternado = new BaseColor(244, 220, 180); // parecido a SandyBrown
            //color para celdas de elaboradas
            if (tipo_planilla == "ELABORADAS")
            {
                colorAlternado = new BaseColor(230, 240, 255); // azul muy suave

            }

            for (int i = 0; i < 11; i++)
            {
                BaseColor color = (i % 2 == 0)
                    ? colorBlanco
                    : colorAlternado;

                PdfPCell celdaA = new PdfPCell(new Phrase("Alm.", fuenteEncabezado));
                celdaA.BackgroundColor = color;
                celdaA.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(celdaA);

                PdfPCell celdaC = new PdfPCell(new Phrase("Cena", fuenteEncabezado));
                celdaC.BackgroundColor = color;
                celdaC.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(celdaC);
            }

                            //agregar celdas
            foreach (DSapMenuEstadistico item in listaSapsCantidades)
            {
                Font fuente = item.fecha == "Totales"
                    ? fuenteTotales
                    : fuenteCelda;

                BaseColor colorFondo = item.fecha == "Totales"
                    ? BaseColor.LIGHT_GRAY
                    : BaseColor.WHITE;


                AgregarCelda(tabla, item.fecha, fuente, item.fecha == "Totales" ? BaseColor.LIGHT_GRAY : BaseColor.WHITE,
                    Element.ALIGN_LEFT);

                // Valores numéricos
                int[] valores =
                {
                    item.P12_A, item.P12_C,
                    item.P24_A, item.P24_C,
                    item.IntN_A, item.IntN_C,
                    item.Astr_A, item.Astr_C,
                    item.Celi_A, item.Celi_C,
                    item.AFib_A, item.AFib_C,
                    item.Hep_A, item.Hep_C,
                    item.SSal_A, item.SSal_C,
                    item.HivTbc_A, item.HivTbc_C,
                    item.Men_A, item.Men_C,
                    item.SobreAl_A, item.SobreAl_C
                };

                //color de las columnas
                BaseColor colorAlternado2 = new BaseColor(244, 220, 180);

                //color para celdas de elaboradas
                if (tipo_planilla == "ELABORADAS")
                {
                    colorAlternado2 = new BaseColor(230, 240, 255); // azul muy suave

                }

                for (int grupo = 0; grupo < 11; grupo++)
                {
                    BaseColor colorGrupo = (grupo % 2 == 0)
                        ? BaseColor.WHITE
                        : colorAlternado2;

                    // Si es la fila Totales, mantener gris
                    if (item.fecha == "Totales")
                        colorGrupo = BaseColor.LIGHT_GRAY;

                    AgregarCelda(tabla, valores[grupo * 2].ToString(), fuente, colorGrupo);

                    AgregarCelda(tabla, valores[grupo * 2 + 1].ToString(), fuente, colorGrupo);
                }
            }
            

            //agregar tabla
            doc.Add(tabla);
        }

        //FIN METODO AGREGAR PAGINA...................................................................

        //METODO AGREGAR PAGINA - tipo_planilla = SOLICITADAS o ELABORADAS
        private static void AgregarPaginaParteNovedades(Document doc, List<DUnidadMenuCantidades> listaUnidadesCantidades, DUnidadGrupo unidadGrupo, string tipo_planilla, string fechaElaborada)
        {

            // Fuentes
            Font fuenteLogo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteTitulo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteTituloTabla = FontFactory.GetFont(FontFactory.TIMES_BOLD, 10);
            Font fuenteEncabezado = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteCelda = FontFactory.GetFont(FontFactory.TIMES, 9);
            Font fuenteTotales = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
                        

            // AQUÍ VENDRÁ LA TABLA
            PdfPTable tabla = new PdfPTable(23);
            tabla.WidthPercentage = 100;

            tabla.SetWidths(new float[]
            {
                3f,      // Unidad

                .7f,.7f, // P12
                .7f,.7f, // P24
                .7f,.7f, // IntN
                .7f,.7f, // Astr
                .7f,.7f, // Celi
                .7f,.7f, // AFib
                .7f,.7f, // Hep
                .7f,.7f, // SSal
                .7f,.7f, // HivTbc
                .7f,.7f, // Men
                .7f,.7f  // SobreAl
            });

            //agregar filas de encabezados
            // UNIDADES
            string titulo_tabla = unidadGrupo.unidad_grupo + "\n\n" + fechaElaborada;
            PdfPCell celdaUnidad = new PdfPCell(new Phrase(titulo_tabla, fuenteTituloTabla));
            celdaUnidad.Rowspan = 3;
            celdaUnidad.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaUnidad.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(celdaUnidad);

            // PERSONAL (12Hs + 24Hs = 4 columnas)
            PdfPCell celdaPersonal = new PdfPCell(new Phrase("PERSONAL", fuenteEncabezado));

            celdaPersonal.Colspan = 4;
            celdaPersonal.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(celdaPersonal);

            // INTERNOS NORMAL (2 columnas)
            PdfPCell celdaInternos = new PdfPCell(new Phrase("Internos\n(Normal)", fuenteEncabezado));
            celdaInternos.Colspan = 2;
            celdaInternos.Rowspan = 2;
            celdaInternos.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaInternos.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(celdaInternos);

            // REGIMEN DIETOTERAPICO (16 columnas)
            PdfPCell celdaRegimen = new PdfPCell(new Phrase("Régimen DIETOTERÁPICO: Personal/Internos", fuenteEncabezado));
            celdaRegimen.Colspan = 16;
            celdaRegimen.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(celdaRegimen);

            //--segunda fila encabezado
            AgregarGrupo(tabla, "12Hs", fuenteEncabezado);
            AgregarGrupo(tabla, "24Hs", fuenteEncabezado);

            AgregarGrupo(tabla, "Dieta\nAstring.", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta\nCelíaco", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta Alta\nen Fibra", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta Hepato\nProtectora", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta\nS/Sal", fuenteEncabezado);
            AgregarGrupo(tabla, "Dieta\nHIV/TBC", fuenteEncabezado);
            AgregarGrupo(tabla, "Menores", fuenteEncabezado);
            AgregarGrupo(tabla, "Sobre\nAlim.", fuenteEncabezado);


            //--tercer fila encabezado

            BaseColor colorBlanco = BaseColor.WHITE;

            //color para celdas de solicitadas
            BaseColor colorAlternado = new BaseColor(244, 220, 180); // parecido a SandyBrown
            //color para celdas de elaboradas
            if (tipo_planilla == "ELABORADAS")
            {
                colorAlternado = new BaseColor(230, 240, 255); // azul muy suave

            }

            for (int i = 0; i < 11; i++)
            {
                BaseColor color = (i % 2 == 0)
                    ? colorBlanco
                    : colorAlternado;

                PdfPCell celdaA = new PdfPCell(new Phrase("Alm.", fuenteEncabezado));
                celdaA.BackgroundColor = color;
                celdaA.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(celdaA);

                PdfPCell celdaC = new PdfPCell(new Phrase("Cena", fuenteEncabezado));
                celdaC.BackgroundColor = color;
                celdaC.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(celdaC);
            }

            //agregar celdas
            foreach (DUnidadMenuCantidades item in listaUnidadesCantidades)
            {
                Font fuente = item.unidad == "Totales"
                    ? fuenteTotales
                    : fuenteCelda;

                BaseColor colorFondo = item.unidad == "Totales"
                    ? BaseColor.LIGHT_GRAY
                    : BaseColor.WHITE;


                AgregarCelda(tabla, item.unidad, fuente, item.unidad == "Totales" ? BaseColor.LIGHT_GRAY : BaseColor.WHITE,
                    Element.ALIGN_LEFT);

                // Valores numéricos
                int[] valores =
                {
                    item.P12_A, item.P12_C,
                    item.P24_A, item.P24_C,
                    item.IntN_A, item.IntN_C,
                    item.Astr_A, item.Astr_C,
                    item.Celi_A, item.Celi_C,
                    item.AFib_A, item.AFib_C,
                    item.Hep_A, item.Hep_C,
                    item.SSal_A, item.SSal_C,
                    item.HivTbc_A, item.HivTbc_C,
                    item.Men_A, item.Men_C,
                    item.SobreAl_A, item.SobreAl_C
                };

                //color de las columnas
                BaseColor colorAlternado2 = new BaseColor(244, 220, 180);

                //color para celdas de elaboradas
                if (tipo_planilla == "ELABORADAS")
                {
                    colorAlternado2 = new BaseColor(230, 240, 255); // azul muy suave

                }

                for (int grupo = 0; grupo < 11; grupo++)
                {
                    BaseColor colorGrupo = (grupo % 2 == 0)
                        ? BaseColor.WHITE
                        : colorAlternado2;

                    // Si es la fila Totales, mantener gris
                    if (item.unidad == "Totales")
                        colorGrupo = BaseColor.LIGHT_GRAY;

                    AgregarCelda(tabla, valores[grupo * 2].ToString(), fuente, colorGrupo);

                    AgregarCelda(tabla, valores[grupo * 2 + 1].ToString(), fuente, colorGrupo);
                }
            }


            //agregar tabla
            doc.Add(tabla);
        }

        //FIN METODO AGREGAR PAGINA...................................................................

        //AGREGAR FILA TOTALES UNIDAD
        private static void AgregarFilaTotales(List<DUnidadMenuCantidades> lista)
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

        //AGREGAR FILA TOTALES SAP
        private static void AgregarFilaTotalesSap(List<DSapMenuCantidades> lista)
        {
            // Evitar duplicar la fila Totales
            lista.RemoveAll(x => x.sap == "Totales");

            var totales = new DSapMenuCantidades
            {
                sap = "Totales",

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

        }
        // FIN AGREGAR FILA TOTALES SAP............................................................

        //AGREGAR FILA TOTALES SAP ESTADISTICO
        private static void AgregarFilaTotalesSapEstadistico(List<DSapMenuEstadistico> lista)
        {
            // Evitar duplicar la fila Totales
            lista.RemoveAll(x => x.fecha == "Totales");

            var totales = new DSapMenuEstadistico
            {
                fecha = "Totales",

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

        }
        // FIN AGREGAR FILA TOTALES SAP ESTADISTICO............................................................

        //AGREGAR FILA TOTALES UNIDAD PARTE NOVEDADES DIARIO
        private static void AgregarFilaTotalesUnidadParteNovedades(List<DUnidadMenuCantidades> lista)
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

        }
        // FIN AGREGAR FILA TOTALES UNIDAD PARTE NOVEDADES DIARIO............................................................


        //AGREGAR GRUPO PARA PLANILLA PARTE DIARIO
        private static void AgregarGrupo(PdfPTable tabla, string texto, Font fuente)
        {
            PdfPCell celda = new PdfPCell(
                new Phrase(texto, fuente));

            celda.Colspan = 2;
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;

            tabla.AddCell(celda);
        }
        //FIN AGREGAR GRUPO PARA PLANILLA PARTE DIARIO.........................................

        //AGREGAR CELDAS PARA PLANILLA PARTE DIARIO
        private static void AgregarCelda(PdfPTable tabla, string texto, Font fuente, BaseColor color, int alineacion = Element.ALIGN_CENTER)
        {
            PdfPCell celda = new PdfPCell(new Phrase(texto, fuente));

            celda.HorizontalAlignment = alineacion;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;
            celda.BackgroundColor = color;

            tabla.AddCell(celda);
        }
        //FIN AGREGAR CELDAS PARA PLANILLA PARTE DIARIO......................................

        //AGREGAR FIRMAS
        private static void AgregarFirmasPiePagina(Document doc, PdfWriter writer)
        {
            Font fuenteFirma = FontFactory.GetFont(FontFactory.TIMES, 8);
            Font fuenteFirmaNegrita = FontFactory.GetFont(FontFactory.TIMES_BOLD, 8);

            PdfPTable tablaFirmas = new PdfPTable(3);
            tablaFirmas.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;

            tablaFirmas.SetWidths(new float[] { 40f, 20f, 40f });

            // Firma izquierda
            PdfPCell firmaIzq = new PdfPCell();
            firmaIzq.Border = Rectangle.NO_BORDER;

            firmaIzq.AddElement(new Paragraph("Lic. Lorena de los Ángeles MEDINA", fuenteFirmaNegrita)
            {
                Alignment = Element.ALIGN_CENTER
            });

            Paragraph p2 = new Paragraph("Alcaide - Jefa de Div. Nutrición", fuenteFirma);
            p2.Alignment = Element.ALIGN_CENTER;
            p2.Leading = 8f; // interlineado
            firmaIzq.AddElement(p2);

            Paragraph p3 = new Paragraph("Resp. del Servicio de Alimentación - S.P.P.S.", fuenteFirma);
            p3.Alignment = Element.ALIGN_CENTER;
            p3.Leading = 8f; // interlineado
            firmaIzq.AddElement(p3);

            tablaFirmas.AddCell(firmaIzq);

            // Centro vacío
            PdfPCell centro = new PdfPCell();
            centro.Border = Rectangle.NO_BORDER;
            tablaFirmas.AddCell(centro);

            // Firma derecha
            PdfPCell firmaDer = new PdfPCell();
            firmaDer.Border = Rectangle.NO_BORDER;

            firmaDer.AddElement(new Paragraph("Renzo Ismael FLORES",fuenteFirmaNegrita)
            {
                Alignment = Element.ALIGN_CENTER
            });

            Paragraph p4 = new Paragraph("Lic. en Nutrición - MP 674", fuenteFirma);
            p4.Alignment = Element.ALIGN_CENTER;
            p4.Leading = 8f; // interlineado
            firmaDer.AddElement(p4);

            Paragraph p5 = new Paragraph("Resp. de Alim. Sano y Bueno Catering", fuenteFirma);
            p5.Alignment = Element.ALIGN_CENTER;
            p5.Leading = 8f; // interlineado
            firmaDer.AddElement(p5);

            tablaFirmas.AddCell(firmaDer);

            tablaFirmas.CompleteRow();

            // Posición fija cerca del borde inferior
            float posY = doc.BottomMargin + 35;

            tablaFirmas.WriteSelectedRows(0, -1, doc.LeftMargin, posY, writer.DirectContent);
        }
        //FIN AGREGAR FIRMAS
    }
}
