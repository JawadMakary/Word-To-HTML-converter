﻿using System.IO;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;

byte[] byteArray = File.ReadAllBytes(@"C:/DIRNAME/fileName.docx");
Console.WriteLine(byteArray.Length);
using (MemoryStream memoryStream = new MemoryStream())
{
    memoryStream.Write(byteArray, 0, byteArray.Length);
    using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
    {
        HtmlConverterSettings settings = new HtmlConverterSettings()
        {
            PageTitle = "My Page Title"
        };
        XElement html = HtmlConverter.ConvertToHtml(doc, settings);

        // Note: the XHTML returned by ConvertToHtmlTransform contains objects of type
        // XEntity. PtOpenXmlUtil.cs defines the XEntity class. See
        // http://blogs.msdn.com/ericwhite/archive/2010/01/21/writing-entity-references-using-linq-to-xml.aspx
        // for detailed explanation.
        //
        // If you further transform the XML tree returned by ConvertToHtmlTransform, you
        // must do it correctly, or entities do not serialize properly.

        File.WriteAllText(@"C:/DIRNAME/fileName.html", html.ToStringNewLineOnAttributes());
    }
}