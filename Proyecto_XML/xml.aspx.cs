using System;
using System.Collections.Generic;
using System.Xml;
using Faker;


class Program
{
    static void Main()
    {
        // Lista para almacenar los datos de los jugadores
        List<JugadorBaloncesto> jugadores = new List<JugadorBaloncesto>();

        // Genera datos ficticios para 50 jugadores de baloncesto
        Random random = new Random();
        for (int i = 1; i <= 50; i++)
        {
            JugadorBaloncesto jugador = new JugadorBaloncesto
            {
                Numero = i.ToString("D3"),
                Fecha = DateTime.Now.AddDays(random.Next(-365, 0)).ToShortDateString(),
                CIF = random.Next(100000000, 1000000000).ToString() + (char)random.Next('A', 'Z'),
                Nombre = Faker.NameFaker.FirstName(),
                Apellido = Faker.NameFaker.LastName(),
                Importe = random.Next(500, 2000) + random.NextDouble(),
                ImporteIVA = random.Next(600, 2420) + random.NextDouble(),
                Moneda = random.Next(2) == 0 ? "USD" : "EUR",
                FechaFactura = DateTime.Now.AddMinutes(random.Next(-100000, 0)).ToString("yyyy-MM-ddTHH:mm:ss"),
                Estado = random.Next(2) == 0 ? "Pagada" : "Pendiente"
            };
            jugadores.Add(jugador);
        }

        // Crea un documento XML
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement root = xmlDoc.CreateElement("facturas");
        xmlDoc.AppendChild(root);

        // Agrega los datos de los jugadores al XML
        foreach (var jugador in jugadores)
        {
            XmlElement facturaElement = xmlDoc.CreateElement("factura");
            facturaElement.AppendChild(CreateElement(xmlDoc, "numero", jugador.Numero));
            facturaElement.AppendChild(CreateElement(xmlDoc, "fecha", jugador.Fecha));
            facturaElement.AppendChild(CreateElement(xmlDoc, "CIF", jugador.CIF));
            facturaElement.AppendChild(CreateElement(xmlDoc, "nombre", jugador.Nombre));
            facturaElement.AppendChild(CreateElement(xmlDoc, "apellido", jugador.Apellido));
            facturaElement.AppendChild(CreateElement(xmlDoc, "importe", jugador.Importe.ToString()));
            facturaElement.AppendChild(CreateElement(xmlDoc, "importeIVA", jugador.ImporteIVA.ToString()));
            facturaElement.AppendChild(CreateElement(xmlDoc, "moneda", jugador.Moneda));
            facturaElement.AppendChild(CreateElement(xmlDoc, "fechaFactura", jugador.FechaFactura));
            facturaElement.AppendChild(CreateElement(xmlDoc, "estado", jugador.Estado));

            root.AppendChild(facturaElement);
        }

        // Guarda el documento XML en un archivo
        xmlDoc.Save("facturas.xml");

        Console.WriteLine("Archivo XML generado con éxito: facturas.xml");
    }

    // Método para crear un elemento XML con un nombre y un valor específicos
    static XmlElement CreateElement(XmlDocument xmlDoc, string name, string value)
    {
        XmlElement element = xmlDoc.CreateElement(name);
        element.InnerText = value;
        return element;
    }
}

// Clase para almacenar los datos de los jugadores
class JugadorBaloncesto
{
    public string Numero { get; set; }
    public string Fecha { get; set; }
    public string CIF { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public double Importe { get; set; }
    public double ImporteIVA { get; set; }
    public string Moneda { get; set; }
    public string FechaFactura { get; set; }
    public string Estado { get; set; }
}
