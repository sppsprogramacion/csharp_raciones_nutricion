using CommonCache;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CapaDatos;
using static System.Net.WebRequestMethods;
using System.Windows.Forms;
using System.Linq;

namespace CapaPresentacion.Reportes
{
    public class ReportesElaboradasPDF
    {
        //PLANILLA RENDICION         
        public static MemoryStream RepPdfPlanillaLiquidacion(List<string>encabezadoPlanilla, List<string[]> filasPlanilla, string total)
        {
            MemoryStream ms = new MemoryStream();
            Document doc = new Document(PageSize.A4.Rotate(), 5, 5, 5, 5);

            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            writer.CloseStream = false; // evita cerrar el MemoryStream al cerrar el documento

            doc.Open();

            var fuenteLogo = FontFactory.GetFont(FontFactory.TIMES, 9, BaseColor.BLACK);
            var fuenteOrganismo = FontFactory.GetFont(FontFactory.TIMES, 9, BaseColor.BLACK);
            var fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK);
            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
            var fuenteEncabezadoTabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.BLACK);
            var fuenteCeldas = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.BLACK);
            var fuenteTotal = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13, BaseColor.BLACK);

            string organismo = "Div. Nutricion";

            // Crear tabla 1 columna
            PdfPTable tablaEncabezado = new PdfPTable(1);
            tablaEncabezado.WidthPercentage = 20; // ocupa 1/5 de la página
            tablaEncabezado.HorizontalAlignment = Element.ALIGN_LEFT; // tabla a la izquierda

            // Centrar contenido de todas las celdas
            tablaEncabezado.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaEncabezado.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tablaEncabezado.DefaultCell.Border = Rectangle.NO_BORDER;

            // Agregar celdas
            tablaEncabezado.AddCell(new Paragraph("  SERVICIO PENITENCIARIO DE LA PROVINCIA DE SALTA", fuenteLogo));
            //celda organismo
            PdfPCell celdaOrganismo = new PdfPCell(new Phrase(organismo, fuenteOrganismo));
            celdaOrganismo.MinimumHeight = 10f;
            celdaOrganismo.PaddingTop = 0f;
            celdaOrganismo.PaddingBottom = 0f;
            celdaOrganismo.Border = Rectangle.NO_BORDER;
            celdaOrganismo.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaEncabezado.AddCell(celdaOrganismo);
            //tablaEncabezado.AddCell(new Paragraph(organismo, fuenteOrganismo));

            // Agregar tabla al documento
            doc.Add(tablaEncabezado);
            //fin logo encabezado.....................................

            //fecha
            DateTime fechaHoy = DateTime.Now;
            CultureInfo cultura = new CultureInfo("es-ES");

            // "d 'de' MMMM 'de' yyyy" → ejemplo: "9 de septiembre de 2025"
            string fechaCompleta = "Salta, " + fechaHoy.ToString("d 'de' MMMM 'de' yyyy", cultura);

            //doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(fechaCompleta, fuenteNormal)
            {
                Alignment = Element.ALIGN_RIGHT
            });
            //fin fecha.............................

            //datos planilla
            Paragraph titulo = new Paragraph("PLANILLA de LIQUIDACION: 85° Rendición ‐ Periodo del 01 al 15 d Enero del 2026", fuenteTitulo);
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);
            doc.Add(new Paragraph(" "));

            PdfPTable tablaPlanilla = new PdfPTable(encabezadoPlanilla.Count);
            tablaPlanilla.WidthPercentage = 100;
            float[] anchos = Enumerable.Repeat(0.8f, encabezadoPlanilla.Count).ToArray();

            anchos[0] = 1.3f;
            anchos[encabezadoPlanilla.Count-1] = 1f;
            tablaPlanilla.SetWidths(anchos);
           
            // encabezado
            foreach (var item in encabezadoPlanilla)
            {
                PdfPCell celda = new PdfPCell(new Phrase(item, fuenteEncabezadoTabla));

                celda.MinimumHeight = 12f;
                celda.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPlanilla.AddCell(celda);

            }

            // Filas dinámicas
            foreach(string[] fila in filasPlanilla)
{
                for (int i = 0; i < fila.Length; i++)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(fila[i], fuenteCeldas));

                    celda.FixedHeight = 11f;
                    celda.PaddingTop = 0f;
                    // Alineación por columna
                    if (i == 0) // primera columna
                    {
                        celda.HorizontalAlignment = Element.ALIGN_LEFT;
                    }                    
                    else // resto
                    {
                        celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    tablaPlanilla.AddCell(celda);
                }
            }
            
            doc.Add(tablaPlanilla);

            // Crear tabla Total 2 columna
            PdfPTable tablaTotal = new PdfPTable(2);
            tablaTotal.WidthPercentage = 20; // ocupa 1/5 de la página
            tablaTotal.HorizontalAlignment = Element.ALIGN_RIGHT; // tabla a la izquierda

            // Centrar contenido de todas las celdas
            tablaTotal.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaTotal.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            // Agregar celdas
            PdfPCell celdaTextoTotal = new PdfPCell(new Phrase("Sub Total:", fuenteTotal));
            celdaTextoTotal.PaddingTop = 3f;
            celdaTextoTotal.PaddingBottom = 0f;
            celdaTextoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaTextoTotal.Border = Rectangle.NO_BORDER;
            tablaTotal.AddCell(celdaTextoTotal);
            //celda organismo
            PdfPCell celdaTotal = new PdfPCell(new Phrase(total, fuenteTotal));
            celdaTotal.PaddingTop = 3f;
            celdaTotal.PaddingBottom = 0f;
            celdaTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaTotal.Border = Rectangle.NO_BORDER;
            tablaTotal.AddCell(celdaTotal);
            //tablaEncabezado.AddCell(new Paragraph(organismo, fuenteOrganismo));

            // Agregar tabla al documento
            doc.Add(tablaTotal);
            //fin logo encabezado.....................................

            doc.Close(); // Cierra el documento pero NO el MemoryStream
            ms.Position = 0;

            return ms;
        }

    }
}
