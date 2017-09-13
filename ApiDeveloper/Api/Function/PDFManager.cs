using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using System.IO;

/**
 * 
 	  @author BeerSting <br>
 	     * <b>The MIT License (MIT) Copyright: </b><br>
		 * Copyright (c) 2017, BeerSting<br>
		 * 
		 * <b>Create by: </b><br>
		 * Yoottapong Wongwiwut<br>  
		 * 
		 * <b>Create Date: </b><br>
		 *  July 07 2017<br>
		 * 
		 * <b>Email: </b><br>
		 * <A href="mailto:beer.sting@gmail.com">beer.sting@gmail.com</A><br> 
	  @version 1.0
 * 
 */

namespace Developer.Api.Function
{

    public class PDFManager
    {
        public void Merge(string destinationfile, string[] fileList)
        {
            //Step 1: Create a Docuement-Object
            Document document = new Document();
            try
            {
                //Step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationfile, FileMode.Create));

                //Step 3: Open the document
                document.Open();

                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;

                int n = 0;
                int rotation = 0;

                //Loops for each file that has been listed
                foreach (string filePath in fileList)
                {
                    string filename = Path.GetFileName(filePath);

                    // we create a reader for the document
                    PdfReader reader = new PdfReader(filePath);

                    //Gets the number of pages to process
                    n = reader.NumberOfPages;

                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(1));
                        document.NewPage();

                        //Insert to Destination on the first page
                        if (i == 1)
                        {
                            Chunk fileRef = new Chunk(" ");
                            fileRef.SetLocalDestination(filename);
                            document.Add(fileRef);
                        }

                        page = writer.GetImportedPage(reader, i);
                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            finally { document.Close(); }
        }

    }
}
