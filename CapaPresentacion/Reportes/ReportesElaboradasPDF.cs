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
        public static MemoryStream RepPdfPlanillaLiquidacionQuincenal(List<string>encabezadoPlanilla, List<string[]> filasPlanilla, string total, List<DPlanillaLiquidacion2da>filasPlanilla2da, int numero_rendicion, string fechaInicio, string fechaFin)
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

            DateTime inicio = Convert.ToDateTime(fechaInicio);
            DateTime fin = Convert.ToDateTime(fechaFin);

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
            Paragraph titulo = new Paragraph("PLANILLA de LIQUIDACION: " + numero_rendicion + "° Rendición ‐ Periodo del " + inicio.ToString("dd/MM/yyyy") + " al " + fin.ToString("dd/MM/yyyy"), fuenteTitulo);
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);
            doc.Add(new Paragraph(" "));

            //UNA pagina cuando son por quincena            
            PdfPTable tablaPlanilla = new PdfPTable(encabezadoPlanilla.Count);
            tablaPlanilla.WidthPercentage = 100;
            float[] anchos = Enumerable.Repeat(0.8f, encabezadoPlanilla.Count).ToArray();

            anchos[0] = 1.3f;
            anchos[encabezadoPlanilla.Count - 1] = 1f;
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
            foreach (string[] fila in filasPlanilla)
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

            // Agregar tabla total al documento
            doc.Add(tablaTotal);

            // --------------------------------- Nueva página ----------------------------------------------
            doc.NewPage();

            // Agregar tabla encabezado al documento
            doc.Add(tablaEncabezado);
            //fin tabla encabezado.....................................

            //fecha.............................
            doc.Add(new Paragraph(fechaCompleta, fuenteNormal)
            {
                Alignment = Element.ALIGN_RIGHT
            });
            //fin fecha.............................

            doc.Add(titulo);
            doc.Add(new Paragraph(" "));

            //tabla planilla 2da
            PdfPTable tabla = new PdfPTable(4);
            tabla.WidthPercentage = 60;
            tabla.HorizontalAlignment = Element.ALIGN_LEFT; // tabla a la izquierda
            tabla.SetWidths(new float[] { 5f, 1.0f, 1.0f, 1.1f });

            Font fuenteEncabezado = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
            Font fuenteFila = FontFactory.GetFont(FontFactory.TIMES, 11);

            string[] encabezados = { "Menus", "SubT.", "Factor", "RACION" };

            foreach (string texto in encabezados)
            {
                PdfPCell celda = new PdfPCell(new Phrase(texto, fuenteEncabezado));

                celda.HorizontalAlignment = Element.ALIGN_CENTER;
                celda.VerticalAlignment = Element.ALIGN_MIDDLE;

                celda.BorderWidth = 1.5f;

                tabla.AddCell(celda);
            }

            decimal total2da = 0;
            int cuenta_filas = 0;

            foreach (DPlanillaLiquidacion2da filaPlanilla in filasPlanilla2da)
            {
                cuenta_filas = cuenta_filas + 1;
                if (cuenta_filas < 4)
                {
                    AgregarFila(tabla, filaPlanilla.menu, filaPlanilla.subtotal.ToString(), filaPlanilla.factor.ToString(), filaPlanilla.racion.ToString(), fuenteFila);
                    total2da = total2da + filaPlanilla.racion;
                }
                else
                {
                    AgregarFila(tabla, filaPlanilla.menu, filaPlanilla.subtotal.ToString(), filaPlanilla.factor.ToString(), filaPlanilla.racion.ToString(), fuenteFila, true);
                    total2da = total2da + filaPlanilla.racion;
                    //AgregarFila(tabla, " ", " ", " ", " ", fuenteFila, true);
                    cuenta_filas = 0;
                }
            }

           
            doc.Add(tabla);

            // Crear tabla tablaTotal2da  columna
            PdfPTable tablaTotal2da = new PdfPTable(3);
            tablaTotal2da.WidthPercentage = 60; // ocupa 1/5 de la página
            tablaTotal2da.HorizontalAlignment = Element.ALIGN_LEFT; // tabla a la izquierda
            tablaTotal2da.SetWidths(new float[] { 5f, 2.0f, 1.1f });

            // Centrar contenido de todas las celdas
            tablaTotal2da.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaTotal2da.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            // Agregar celdas
            PdfPCell celdaVacia = new PdfPCell(new Phrase(" ", fuenteTotal));
            celdaVacia.PaddingTop = 3f;
            celdaVacia.PaddingBottom = 0f;
            celdaVacia.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaVacia.Border = Rectangle.NO_BORDER;
            tablaTotal2da.AddCell(celdaVacia);

            PdfPCell celdaTextoTotal2da = new PdfPCell(new Phrase("TOTAL FINAL:", fuenteTotal));
            celdaTextoTotal2da.PaddingTop = 3f;
            celdaTextoTotal2da.PaddingBottom = 0f;
            celdaTextoTotal2da.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaTextoTotal2da.Border = Rectangle.NO_BORDER;
            tablaTotal2da.AddCell(celdaTextoTotal2da);
            //celda organismo
            PdfPCell celdaTotal2da = new PdfPCell(new Phrase(total, fuenteTotal));
            celdaTotal2da.PaddingTop = 3f;
            celdaTotal2da.PaddingBottom = 0f;
            celdaTotal2da.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaTotal2da.Border = Rectangle.NO_BORDER;
            tablaTotal2da.AddCell(celdaTotal2da);
            //tablaEncabezado.AddCell(new Paragraph(organismo, fuenteOrganismo));

            // Agregar tabla total al documento
            doc.Add(tablaTotal2da);

            doc.Close(); // Cierra el documento pero NO el MemoryStream
            ms.Position = 0;

            return ms;
        }
        //FIN PLANILLA RENDICION QUINCENAL................................................................

        //PLANILLA RENDICION MENDUAL     
        public static MemoryStream RepPdfPlanillaLiquidacionMensual(List<string> encabezadoPlanilla, List<string[]> filasPlanilla, string total, List<DPlanillaLiquidacion2da> filasPlanilla2da, int numero_rendicion, string fechaInicio, string fechaFin)
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

            DateTime inicio = Convert.ToDateTime(fechaInicio);
            DateTime fin = Convert.ToDateTime(fechaFin);

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
            Paragraph titulo = new Paragraph("PLANILLA de LIQUIDACION: " + numero_rendicion + "° Rendición ‐ Periodo del " + inicio.ToString("dd/MM/yyyy") + " al " + fin.ToString("dd/MM/yyyy"), fuenteTitulo);

            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);
            doc.Add(new Paragraph(" "));
                       

            //DOS paginas cuando son por mes
            
            //PRIMERA pagina
            PdfPTable tablaPlanilla = new PdfPTable(19);
            tablaPlanilla.WidthPercentage = 100;
            float[] anchos = Enumerable.Repeat(0.8f, 19).ToArray();

            anchos[0] = 1.3f;
            anchos[18] = 1f;
            tablaPlanilla.SetWidths(anchos);

            // encabezado
            foreach (var item in encabezadoPlanilla.Take(16))
            {
                PdfPCell celda = new PdfPCell(new Phrase(item, fuenteEncabezadoTabla));

                celda.MinimumHeight = 12f;
                celda.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPlanilla.AddCell(celda);
            }

            PdfPCell celdaSubtotal = new PdfPCell(new Phrase("Subtotal.", fuenteEncabezadoTabla));
            tablaPlanilla.AddCell(celdaSubtotal);
            PdfPCell celdaFactor = new PdfPCell(new Phrase("Factor", fuenteEncabezadoTabla));
            tablaPlanilla.AddCell(celdaFactor);
            PdfPCell celdaRacion = new PdfPCell(new Phrase("RACION", fuenteEncabezadoTabla));
            tablaPlanilla.AddCell(celdaRacion);

            decimal subTotalPLanillaHoja1 = 0;
            // Filas dinámicas
            foreach (string[] fila in filasPlanilla)
            {
                int subTotal1 = 0;
                for (int i = 0; i < 16; i++)
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

                    if (i > 0)
                    {
                        subTotal1 = subTotal1 + Convert.ToInt32(fila[i].ToString());
                    }
                }

                //celda subtotal
                PdfPCell celdaSubtotalCantidad = new PdfPCell(new Phrase(subTotal1.ToString(), fuenteCeldas));
                celdaSubtotalCantidad.HorizontalAlignment = Element.ALIGN_RIGHT;
                celdaSubtotalCantidad.FixedHeight = 11f;
                celdaSubtotalCantidad.PaddingTop = 0f;
                tablaPlanilla.AddCell(celdaSubtotalCantidad);
                
                //celda factor
                decimal factor = Convert.ToDecimal(fila[fila.Length - 2]);
                PdfPCell celdaFactorCantidad = new PdfPCell(new Phrase(factor.ToString() , fuenteCeldas));
                celdaFactorCantidad.HorizontalAlignment = Element.ALIGN_RIGHT;
                celdaFactorCantidad.FixedHeight = 11f;
                celdaFactorCantidad.PaddingTop = 0f;
                tablaPlanilla.AddCell(celdaFactorCantidad);
                
                //celda racion
                decimal racionTotal = subTotal1 * factor;
                PdfPCell celdaRacionTotal = new PdfPCell(new Phrase(racionTotal.ToString(), fuenteCeldas));
                celdaRacionTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
                celdaRacionTotal.FixedHeight = 11f;
                celdaRacionTotal.PaddingTop = 0f;
                tablaPlanilla.AddCell(celdaRacionTotal);
                
                subTotalPLanillaHoja1 = subTotalPLanillaHoja1 + racionTotal;
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
            PdfPCell celdaTotal = new PdfPCell(new Phrase(subTotalPLanillaHoja1.ToString(), fuenteTotal));
            celdaTotal.PaddingTop = 3f;
            celdaTotal.PaddingBottom = 0f;
            celdaTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaTotal.Border = Rectangle.NO_BORDER;
            tablaTotal.AddCell(celdaTotal);
            //tablaEncabezado.AddCell(new Paragraph(organismo, fuenteOrganismo));

            // Agregar tabla total al documento
            doc.Add(tablaTotal);

            // --------------------------------- Nueva página 2 ----------------------------------------------
            doc.NewPage();

            // Agregar tabla encabezado al documento
            doc.Add(tablaEncabezado);
            //fin tabla encabezado.....................................

            //fecha.............................
            doc.Add(new Paragraph(fechaCompleta, fuenteNormal)
            {
                Alignment = Element.ALIGN_RIGHT
            });
            //fin fecha.............................

            doc.Add(titulo);
            doc.Add(new Paragraph(" "));

            //SEGUNDA pagina
            PdfPTable tablaPlanilla2 = new PdfPTable(encabezadoPlanilla.Count - 15);
            tablaPlanilla2.WidthPercentage = 100;
            float[] anchos2 = Enumerable.Repeat(0.8f, encabezadoPlanilla.Count - 15).ToArray();

            anchos2[0] = 1.3f;
            anchos2[encabezadoPlanilla.Count - 16] = 1f;
            tablaPlanilla2.SetWidths(anchos2);

            PdfPCell celdaEncabezadoMenu = new PdfPCell(new Phrase("Menus", fuenteEncabezadoTabla));
            celdaEncabezadoMenu.MinimumHeight = 12f;
            celdaEncabezadoMenu.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaPlanilla2.AddCell(celdaEncabezadoMenu);

            // encabezado (toma desde el 16)
            foreach (var item in encabezadoPlanilla.Skip(16))
            {
                PdfPCell celda = new PdfPCell(new Phrase(item, fuenteEncabezadoTabla));

                celda.MinimumHeight = 12f;
                celda.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPlanilla2.AddCell(celda);
            }
            
            decimal subTotalPLanillaHoja2 = 0;
            // Filas dinámicas
            foreach (string[] fila in filasPlanilla)
            {
                PdfPCell celdaMenu = new PdfPCell(new Phrase(fila[0], fuenteCeldas));
                celdaMenu.FixedHeight = 11f;
                celdaMenu.PaddingTop = 0f;
                celdaMenu.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaPlanilla2.AddCell(celdaMenu);

                int subTotal2 = 0;
                for (int i = 16; i < fila.Length-3; i++)
                {
                    PdfPCell celda2 = new PdfPCell(new Phrase(fila[i], fuenteCeldas));

                    celda2.FixedHeight = 11f;
                    celda2.PaddingTop = 0f;
                    // Alineación por columna
                    if (i == 0) // primera columna
                    {
                        celda2.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    else // resto
                    {
                        celda2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    tablaPlanilla2.AddCell(celda2);

                    if (i > 0)
                    {
                        subTotal2 = subTotal2 + Convert.ToInt32(fila[i].ToString());
                    }
                }

                //celda subtotal
                PdfPCell celdaSubtotalCantidad2 = new PdfPCell(new Phrase(subTotal2.ToString(), fuenteCeldas));
                celdaSubtotalCantidad2.FixedHeight = 11f;
                celdaSubtotalCantidad2.PaddingTop = 0f;
                celdaSubtotalCantidad2.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaPlanilla2.AddCell(celdaSubtotalCantidad2);
                
                //celda factor
                decimal factor2 = Convert.ToDecimal(fila[fila.Length - 2]);
                PdfPCell celdaFactorCantidad2 = new PdfPCell(new Phrase(factor2.ToString(), fuenteCeldas));
                celdaFactorCantidad2.FixedHeight = 11f;
                celdaFactorCantidad2.PaddingTop = 0f;
                celdaFactorCantidad2.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaPlanilla2.AddCell(celdaFactorCantidad2);
                
                //celda racion
                decimal racionTotal2 = subTotal2 * factor2;
                PdfPCell celdaRacionTotal = new PdfPCell(new Phrase(racionTotal2.ToString(), fuenteCeldas));
                celdaRacionTotal.FixedHeight = 11f;
                celdaRacionTotal.PaddingTop = 0f;
                celdaRacionTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaPlanilla2.AddCell(celdaRacionTotal);

                subTotalPLanillaHoja2 = subTotalPLanillaHoja2 + racionTotal2;
            }

            doc.Add(tablaPlanilla2);

            // Crear tabla Total 2 columna
            PdfPTable tablaTotal2 = new PdfPTable(2);
            tablaTotal2.WidthPercentage = 20; // ocupa 1/5 de la página
            tablaTotal2.HorizontalAlignment = Element.ALIGN_RIGHT; // tabla a la izquierda

            // Centrar contenido de todas las celdas
            tablaTotal2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaTotal2.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            // Agregar celdas
            PdfPCell celdaTextoTotal2 = new PdfPCell(new Phrase("Sub Total:", fuenteTotal));
            celdaTextoTotal2.PaddingTop = 3f;
            celdaTextoTotal2.PaddingBottom = 0f;
            celdaTextoTotal2.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaTextoTotal2.Border = Rectangle.NO_BORDER;
            tablaTotal2.AddCell(celdaTextoTotal2);
            //celda organismo
            PdfPCell celdaTotal2 = new PdfPCell(new Phrase(subTotalPLanillaHoja2.ToString(), fuenteTotal));
            celdaTotal2.PaddingTop = 3f;
            celdaTotal2.PaddingBottom = 0f;
            celdaTotal2.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaTotal2.Border = Rectangle.NO_BORDER;
            tablaTotal2.AddCell(celdaTotal2);
            //tablaEncabezado.AddCell(new Paragraph(organismo, fuenteOrganismo));

            // Agregar tabla total al documento
            doc.Add(tablaTotal2);

            // --------------------------------- Nueva página 4 ----------------------------------------------
            doc.NewPage();

            // Agregar tabla encabezado al documento
            doc.Add(tablaEncabezado);
            //fin tabla encabezado.....................................

            //fecha.............................
            doc.Add(new Paragraph(fechaCompleta, fuenteNormal)
            {
                Alignment = Element.ALIGN_RIGHT
            });
            //fin fecha.............................

            doc.Add(titulo);
            doc.Add(new Paragraph(" "));

            //tabla planilla 2da
            PdfPTable tabla = new PdfPTable(4);
            tabla.WidthPercentage = 50;
            tabla.HorizontalAlignment = Element.ALIGN_LEFT; // tabla a la izquierda
            tabla.SetWidths(new float[] { 5f, 1.0f, 1.0f, 1.1f });

            Font fuenteEncabezado = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
            Font fuenteFila = FontFactory.GetFont(FontFactory.TIMES, 11);

            string[] encabezados = { "Menus", "SubT.", "Factor", "RACION" };

            foreach (string texto in encabezados)
            {
                PdfPCell celda = new PdfPCell(new Phrase(texto, fuenteEncabezado));

                celda.HorizontalAlignment = Element.ALIGN_CENTER;
                celda.VerticalAlignment = Element.ALIGN_MIDDLE;

                celda.BorderWidth = 1.5f;

                tabla.AddCell(celda);
            }

            decimal total2da = 0;
            int cuenta_filas = 0;

            foreach (DPlanillaLiquidacion2da filaPlanilla in filasPlanilla2da)
            {
                cuenta_filas = cuenta_filas + 1;
                if (cuenta_filas < 4)
                {
                    AgregarFila(tabla, filaPlanilla.menu, filaPlanilla.subtotal.ToString(), filaPlanilla.factor.ToString(), filaPlanilla.racion.ToString(), fuenteFila);
                    total2da = total2da + filaPlanilla.racion;
                }
                else
                {
                    AgregarFila(tabla, filaPlanilla.menu, filaPlanilla.subtotal.ToString(), filaPlanilla.factor.ToString(), filaPlanilla.racion.ToString(), fuenteFila, true);
                    total2da = total2da + filaPlanilla.racion;
                    //AgregarFila(tabla, " ", " ", " ", " ", fuenteFila, true);
                    cuenta_filas = 0;
                }
            }

            //crear imagen de aclaraciones
            System.Drawing.Image imgAclaraciones = Properties.Resources.imagen_aclaracion;
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgAclaraciones,System.Drawing.Imaging.ImageFormat.Png);
            // Ajustar tamaño
            img.ScaleToFit(380f, 400f);

            //Tabla que contendra tabla planilla y imagen aclaraciones
            PdfPTable contenedor = new PdfPTable(2);
            contenedor.WidthPercentage = 100;
            contenedor.SetWidths(new float[] { 50f, 50f });

            //columna izquierda - tabla planilla
            PdfPCell celdaTabla = new PdfPCell(tabla);
            celdaTabla.Border = Rectangle.NO_BORDER;
            celdaTabla.VerticalAlignment = Element.ALIGN_TOP;

            contenedor.AddCell(celdaTabla);

            //columna derecha - imagen aclaraciones
            PdfPCell celdaImagen = new PdfPCell(img);
            celdaImagen.Border = Rectangle.NO_BORDER;
            celdaImagen.HorizontalAlignment = Element.ALIGN_LEFT;
            celdaImagen.VerticalAlignment = Element.ALIGN_TOP;
            celdaImagen.PaddingLeft = 5f;   // separación desde el borde izquierdo

            contenedor.AddCell(celdaImagen);

            //agregar contenedor al documentp
            doc.Add(contenedor);

            // Crear tabla tablaTotal2da  columna
            PdfPTable tablaTotal2da = new PdfPTable(2);
            tablaTotal2da.WidthPercentage = 50; // ocupa 1/5 de la página
            tablaTotal2da.HorizontalAlignment = Element.ALIGN_LEFT; // tabla a la izquierda
            tablaTotal2da.SetWidths(new float[] { 6f, 2.1f });

            // Centrar contenido de todas las celdas
            tablaTotal2da.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaTotal2da.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            // Agregar celdas
            //PdfPCell celdaVacia = new PdfPCell(new Phrase(" ", fuenteTotal));
            //celdaVacia.PaddingTop = 3f;
            //celdaVacia.PaddingBottom = 0f;
            //celdaVacia.HorizontalAlignment = Element.ALIGN_CENTER;
            //celdaVacia.Border = Rectangle.NO_BORDER;
            //tablaTotal2da.AddCell(celdaVacia);

            PdfPCell celdaTextoTotal2da = new PdfPCell(new Phrase("TOTAL FINAL:", fuenteTotal));
            celdaTextoTotal2da.PaddingTop = 3f;
            celdaTextoTotal2da.PaddingBottom = 0f;
            celdaTextoTotal2da.HorizontalAlignment = Element.ALIGN_RIGHT;
            celdaTextoTotal2da.Border = Rectangle.NO_BORDER;
            tablaTotal2da.AddCell(celdaTextoTotal2da);
            //celda organismo
            PdfPCell celdaTotal2da = new PdfPCell(new Phrase(total, fuenteTotal));
            celdaTotal2da.PaddingTop = 3f;
            celdaTotal2da.PaddingBottom = 0f;
            celdaTotal2da.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaTotal2da.Border = Rectangle.NO_BORDER;
            tablaTotal2da.AddCell(celdaTotal2da);
            //tablaEncabezado.AddCell(new Paragraph(organismo, fuenteOrganismo));

            // Agregar tabla total al documento
            doc.Add(tablaTotal2da);

            doc.Close(); // Cierra el documento pero NO el MemoryStream
            ms.Position = 0;

            return ms;
        }
        //FIN PLANILLA RENDICION MENDUAL..................................................................  

        //PLANILLA PARTE DIARIO
        public static MemoryStream RepPdfPlanillaParteDiario(List<DUnidadMenuCantidades> listaUnidadesCantidades)
        {
            MemoryStream ms = new MemoryStream();

            Document doc = new Document(PageSize.A4.Rotate(), 5, 5, 5, 5);

            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            writer.CloseStream = false;

            doc.Open();

            // Fuentes
            Font fuenteLogo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 8);
            Font fuenteOrganismo = FontFactory.GetFont(FontFactory.TIMES, 8);
            Font fuenteTitulo = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            Font fuenteEncabezado = FontFactory.GetFont(FontFactory.TIMES_BOLD, 7);
            Font fuenteCelda = FontFactory.GetFont(FontFactory.TIMES, 7);
            Font fuenteTotales = FontFactory.GetFont(FontFactory.TIMES_BOLD, 7);

            // Encabezado
            PdfPTable tablaEncabezado = new PdfPTable(2);
            tablaEncabezado.WidthPercentage = 100;
            tablaEncabezado.SetWidths(new float[] { 30f, 70f });

            PdfPCell celdaIzq = new PdfPCell(
                new Phrase(
                    "SERVICIO PENITENCIARIO DE LA\nPROVINCIA DE SALTA\nDiv. Nutrición",
                    fuenteLogo));

            celdaIzq.Border = Rectangle.NO_BORDER;
            celdaIzq.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell celdaFecha = new PdfPCell(
                new Phrase(
                    "Salta, " + DateTime.Now.ToString("dddd d 'de' MMMM 'de' yyyy"),
                    fuenteLogo));

            celdaFecha.Border = Rectangle.NO_BORDER;
            celdaFecha.HorizontalAlignment = Element.ALIGN_RIGHT;

            tablaEncabezado.AddCell(celdaIzq);
            tablaEncabezado.AddCell(celdaFecha);

            doc.Add(tablaEncabezado);

            Paragraph titulo = new Paragraph(
                "Raciones SOLICITADAS",
                fuenteTitulo);

            titulo.Alignment = Element.ALIGN_CENTER;

            doc.Add(titulo);

            doc.Add(new Paragraph(" "));

            // AQUÍ VENDRÁ LA TABLA
            PdfPTable tabla = new PdfPTable(23);
            tabla.WidthPercentage = 100;

            tabla.SetWidths(new float[]
            {
                4f,      // Unidad

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
            PdfPCell celdaUnidad = new PdfPCell(new Phrase("Unidades\nCarcelarias", fuenteEncabezado));
            celdaUnidad.Rowspan = 3;
            celdaUnidad.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaUnidad.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(celdaUnidad);

            // PERSONAL (12Hs + 24Hs = 4 columnas)
            PdfPCell celdaPersonal = new PdfPCell(
                new Phrase("PERSONAL", fuenteEncabezado));

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
            PdfPCell celdaRegimen = new PdfPCell(new Phrase("Régimen DIETOTERÁPICO: Personal/Internos",fuenteEncabezado));
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
            BaseColor colorAlternado = new BaseColor(244, 220, 180); // parecido a SandyBrown
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

                for (int grupo = 0; grupo < 11; grupo++)
                {
                    BaseColor colorGrupo = (grupo % 2 == 0)
                        ? BaseColor.WHITE
                        : colorAlternado2;

                    // Si es la fila Totales, mantener gris
                    if (item.unidad == "Totales")
                        colorGrupo = BaseColor.LIGHT_GRAY;

                    AgregarCelda(tabla,valores[grupo * 2].ToString(),fuente,colorGrupo);

                    AgregarCelda(tabla, valores[grupo * 2 + 1].ToString(), fuente, colorGrupo);
                }
            }

            //agregar tabla
            doc.Add(tabla);

            doc.Close();

            ms.Position = 0;

            return ms;
        }
        //FIN PLANILLA PARTE DIARIO

        //AGREGAR FILA
        private static void AgregarFila(
            PdfPTable tabla, string menu, string subT, string factor, string racion, Font fuente, bool lineaSeparadora = false)
        {
            PdfPCell c1 = new PdfPCell(new Phrase(menu, fuente));
            PdfPCell c2 = new PdfPCell(new Phrase(subT, fuente));
            PdfPCell c3 = new PdfPCell(new Phrase(factor, fuente));
            PdfPCell c4 = new PdfPCell(new Phrase(racion, fuente));

            c1.HorizontalAlignment = Element.ALIGN_LEFT;
            c2.HorizontalAlignment = Element.ALIGN_RIGHT;
            c3.HorizontalAlignment = Element.ALIGN_RIGHT;
            c4.HorizontalAlignment = Element.ALIGN_RIGHT;

            c1.Padding = c2.Padding = c3.Padding = c4.Padding = 1f;

            // Bordes laterales
            c1.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            c2.Border = Rectangle.RIGHT_BORDER;
            c3.Border = Rectangle.RIGHT_BORDER;
            c4.Border = Rectangle.RIGHT_BORDER;

            // Línea horizontal al final del grupo
            if (lineaSeparadora)
            {
                c1.Border |= Rectangle.BOTTOM_BORDER;
                c2.Border |= Rectangle.BOTTOM_BORDER;
                c3.Border |= Rectangle.BOTTOM_BORDER;
                c4.Border |= Rectangle.BOTTOM_BORDER;

                c1.BorderWidthBottom = 1.5f;
                c2.BorderWidthBottom = 1.5f;
                c3.BorderWidthBottom = 1.5f;
                c4.BorderWidthBottom = 1.5f;
            }

            tabla.AddCell(c1);
            tabla.AddCell(c2);
            tabla.AddCell(c3);
            tabla.AddCell(c4);
        }
        //FIN AGREGAR FILA..................................................................

        //AGREGAR GRUPO PARA PLANILLA PARTE DIARIO
        private static void AgregarGrupo(PdfPTable tabla,string texto,Font fuente)
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
            PdfPCell celda = new PdfPCell(
                new Phrase(texto, fuente));

            celda.HorizontalAlignment = alineacion;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;
            celda.BackgroundColor = color;

            tabla.AddCell(celda);
        }
        //FIN AGREGAR CELDAS PARA PLANILLA PARTE DIARIO......................................

    }
}
