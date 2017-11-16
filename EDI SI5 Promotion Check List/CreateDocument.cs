using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word =  Microsoft.Office.Interop.Word;

namespace EDI_SI5_Promotion_Check_List
{
    class CreateDocument
    {
        public CreateDocument(String[] Section_1, String[] Section_2, String[] Section_3, String[] Section_4, String[] SignaturAndDate)
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */



            //Start Word and create a new document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = false;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
            ref oMissing, ref oMissing);


            object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            //Section 0: Title Section
            Word.Paragraph Title_Section;
            Title_Section = oDoc.Content.Paragraphs.Add(ref oMissing);
            Title_Section.Range.Font.Size = 16;
            Title_Section.Range.Bold = 1;
            Title_Section.Range.Text = "e-Commerce/EDI SI5 Promotion Check List";
            Title_Section.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            Title_Section.Range.InsertParagraphAfter();


            //Section 1: User Partner Section---------------------------------------------------------------------------------------------------------
            Word.Paragraph UserPartner_Section;
            UserPartner_Section = oDoc.Content.Paragraphs.Add(ref oMissing);
            UserPartner_Section.Range.Text = "User: " + Section_1[0];
            UserPartner_Section.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            UserPartner_Section.Range.InsertAfter("\t\t");
            UserPartner_Section.Range.InsertAfter("Partner: " + Section_1[1]);
            UserPartner_Section.Range.InsertAfter("\t\t");
            UserPartner_Section.Range.InsertAfter("Date: " + Section_1[2]);
            UserPartner_Section.Range.InsertAfter("\n");
            UserPartner_Section.Range.InsertAfter("Title:  " + Section_1[3]);
            UserPartner_Section.Range.InsertAfter("\t\t");
            UserPartner_Section.Range.InsertAfter("Change Management Request Number: " + Section_1[4]);
            UserPartner_Section.Range.InsertParagraphAfter();



            //Section 2: User Signoff Checklist Section------------------------------------------------------------------------------------------------------
            Word.Table UserCheckList;
            Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            UserCheckList = oDoc.Tables.Add(wrdRng, 10, 4, ref oMissing, ref oMissing);
            UserCheckList.Range.ParagraphFormat.SpaceAfter = 6;
            UserCheckList.AllowAutoFit = true;
            UserCheckList.Columns[1].SetWidth(oWord.CentimetersToPoints(6f), Word.WdRulerStyle.wdAdjustNone);
            UserCheckList.Range.Font.Size = 10;
            Word.Column first = UserCheckList.Columns[1];
            int checklist = 0;
            for (int r = 1; r <= 10; r++)
            {
                for (int c = 1; c <= 4; c++)
                {

                    UserCheckList.Cell(r, c).Range.Text = Section_2[checklist];
                    checklist++;
                }
            }



            //Section 3: User Signoff Checklist Section--------------------------------------------------------------------------------------------------------------
            //Insert another paragraph.
            Word.Paragraph oPara3;
            oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara3 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara3.Range.Text = " ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            oPara3.Range.Font.Bold = 0;
            oPara3.Range.Font.Size = 8;
            oPara3.Range.InsertParagraphAfter();

            Word.Table Checklist;

            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara3.Range.Paragraphs.LineSpacing = 10f;
            Checklist = oDoc.Tables.Add(wrdRng, 15, 4, ref oMissing, ref oMissing);
            Checklist.Range.ParagraphFormat.SpaceAfter = 6;
            Checklist.Columns[1].SetWidth(oWord.CentimetersToPoints(5f), Word.WdRulerStyle.wdAdjustNone);
            Checklist.AllowAutoFit = true;

            checklist = 0;
            for (int r = 1; r <= 15; r++)
            {
                for (int c = 1; c <= 4; c++)
                {

                    Checklist.Cell(r, c).Range.Text = Section_3[checklist];
                    checklist++;
                }
            }

            //Section 4: Schedule  
            //Insert another paragraph.
            Word.Paragraph oPara4;
            oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara4 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara4.Range.Font.Size = 8;
            oPara4.Range.Text = "--------------------------------------------------------------------------------------------------------------------------------------------------------";
            oPara4.Range.Font.Bold = 0;

            oPara4.Range.InsertParagraphAfter();



            Word.Table FinalCheckList;

            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara4.Range.Paragraphs.LineSpacing = 10f;
            FinalCheckList = oDoc.Tables.Add(wrdRng, 3, 4, ref oMissing, ref oMissing);
            FinalCheckList.Range.ParagraphFormat.SpaceAfter = 6;
            FinalCheckList.Columns[1].SetWidth(oWord.CentimetersToPoints(5f), Word.WdRulerStyle.wdAdjustNone);
            FinalCheckList.AllowAutoFit = true;



            checklist = 0;
            for (int r = 1; r <= 3; r++)
            {
                for (int c = 1; c <= 4; c++)
                {

                    FinalCheckList.Cell(r, c).Range.Text = Section_4[checklist];
                    checklist++;
                }
            }


            //Signature and Date Section
            //Insert another paragraph.
            Word.Paragraph SignatureAndDate_Section;
            oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            SignatureAndDate_Section = oDoc.Content.Paragraphs.Add(ref oRng);
            SignatureAndDate_Section.Range.Font.Bold = 0;
            SignatureAndDate_Section.Range.InsertParagraphAfter();
            SignatureAndDate_Section.Range.Text = "\n\t\t\t\t\t\t\tProject Manager: " + SignaturAndDate[0];
            SignatureAndDate_Section.Range.InsertAfter("\n\n");
            SignatureAndDate_Section.Range.InsertAfter("\t\t\t\t\t\t\tCompletion Date: " + SignaturAndDate[1]);
            String filename = @"C:\SharePoint Upload\Checklist_" + Section_2[37].Replace(" ", "") + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".docx";
            oWord.ActiveDocument.SaveAs2(filename);//Grabs the Implementation from Post Implementation
            oWord.ActiveDocument.Close();
        }

    }
}


