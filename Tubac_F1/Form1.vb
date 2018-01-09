Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Drawing.Text
Imports System.IO
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports CrystalDecisions.ReportSource
Imports System.Web.UI.WebControls

Public Class Form1

    Private _Font As Font
    Private PATH_FONTS As String = Application.StartupPath + "\Fonts"


    Private Sub btPrint_Click_1(sender As Object, e As EventArgs) Handles btPrint.Click
        'PrintDialog2.Document = PrintDocument2
        'PrintDocument2.Print()
        Dim Report1 As New CrystalDecisions.CrystalReports.Engine.ReportDocument()

        Report1.Load(Application.StartupPath + "\Report\Informe.rpt", CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault.OpenReportByDefault)
        'Report1.SetParameterValue("crTextBox", txtBarcode.Text)
        Report1.SetParameterValue("CodBatch", txtBarcode.Text)

        Report1.SetParameterValue("descripcion", "CUADRADO 1/2''")
        Report1.SetParameterValue("anchotira", "48.01")
        Report1.SetParameterValue("pesoreal", "12.1")
        Report1.SetParameterValue("bobina", "CE120-73-45-05")
        Report1.SetParameterValue("heat", "170265")
        Report1.SetParameterValue("coil", "C86290-4")
        Report1.SetParameterValue("ordencorte", "27853")
        Report1.SetParameterValue("fechacorte", "15/11/17")

        'CrystalReportViewer1.ReportSource = Report1
        Report1.PrintToPrinter(1, False, 0, 0)

        'Dim Report2 As New CrystalDecisions.CrystalReports.Engine.ReportDocument()

        'Report2.Load("C:\Users\Cristhiam\Desktop\Informe1.rpt", CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault.OpenReportByDefault)
        'Report2.SetParameterValue("crTextBox", txtBarcode.Text)
        'Report2.SetParameterValue("CodBatch", txtBarcode.Text)

        'Report2.SetParameterValue("descripcion", "CUADRADO 1/2''")
        'Report2.SetParameterValue("anchotira", "48.01")
        'Report2.SetParameterValue("pesoreal", "12.1")
        'Report2.SetParameterValue("bobina", "CE120-73-45-05")
        'Report2.SetParameterValue("heat", "170265")
        'Report2.SetParameterValue("coil", "C86290-4")
        'Report2.SetParameterValue("ordencorte", "27853")
        'Report2.SetParameterValue("fechacorte", "15/11/17")

        'CrystalReportViewer1.ReportSource = Report2


    End Sub

    Private Sub button1_Click(sender As Object, e As System.EventArgs) Handles button1.Click
        If txtBarcode.Text = String.Empty Then
            lbBarCode.Text = "Tienes que introducir un Código"
        Else

            lbBarCode.Font = _Font
            lbBarCode.Text = FormatBarCode(txtBarcode.Text)
            lbCode.Text = txtBarcode.Text
        End If
    End Sub

    Private Function FormatBarCode(code As String)
        Dim barcode As String = String.Empty
        barcode = String.Format("*{0}*", code)
        Return barcode
    End Function

    Private Sub PrintDocument2_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage
        e.Graphics.DrawString("Muestra de Impresión código de Barras", New Font("Arial", 10, FontStyle.Bold), Brushes.Black, 30, 125)
        e.Graphics.DrawString(FormatBarCode(txtBarcode.Text), _Font, Brushes.Black, 50, 60)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim pfc As PrivateFontCollection = New PrivateFontCollection()
        pfc.AddFontFile(PATH_FONTS & "\BARCOD39.TTF")
        Dim fontFamily As FontFamily = pfc.Families(0)
        _Font = New Font(fontFamily, 30)
    End Sub

End Class