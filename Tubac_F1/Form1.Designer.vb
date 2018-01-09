<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btPrint = New System.Windows.Forms.Button()
        Me.lbCode = New System.Windows.Forms.Label()
        Me.lbBarCode = New System.Windows.Forms.Label()
        Me.button1 = New System.Windows.Forms.Button()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.PrintDialog2 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument2 = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog2 = New System.Windows.Forms.PrintPreviewDialog()
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.reportDocument1 = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        Me.SuspendLayout()
        '
        'btPrint
        '
        Me.btPrint.Location = New System.Drawing.Point(139, 38)
        Me.btPrint.Name = "btPrint"
        Me.btPrint.Size = New System.Drawing.Size(75, 23)
        Me.btPrint.TabIndex = 11
        Me.btPrint.Text = "Imprimir"
        '
        'lbCode
        '
        Me.lbCode.Location = New System.Drawing.Point(12, 106)
        Me.lbCode.Name = "lbCode"
        Me.lbCode.Size = New System.Drawing.Size(248, 24)
        Me.lbCode.TabIndex = 9
        Me.lbCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbBarCode
        '
        Me.lbBarCode.Location = New System.Drawing.Point(12, 66)
        Me.lbBarCode.Name = "lbBarCode"
        Me.lbBarCode.Size = New System.Drawing.Size(248, 40)
        Me.lbBarCode.TabIndex = 8
        Me.lbBarCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'button1
        '
        Me.button1.Location = New System.Drawing.Point(59, 38)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(75, 23)
        Me.button1.TabIndex = 6
        Me.button1.Text = "Generar"
        '
        'txtBarcode
        '
        Me.txtBarcode.Location = New System.Drawing.Point(81, 12)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(112, 20)
        Me.txtBarcode.TabIndex = 7
        Me.txtBarcode.Text = "123456789011"
        Me.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PrintDialog2
        '
        Me.PrintDialog2.UseEXDialog = True
        '
        'PrintDocument2
        '
        '
        'PrintPreviewDialog2
        '
        Me.PrintPreviewDialog2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog2.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog2.Enabled = True
        Me.PrintPreviewDialog2.Icon = CType(resources.GetObject("PrintPreviewDialog2.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog2.Name = "PrintPreviewDialog2"
        Me.PrintPreviewDialog2.Visible = False
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(277, 12)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(375, 288)
        Me.CrystalReportViewer1.TabIndex = 12
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 312)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.btPrint)
        Me.Controls.Add(Me.lbCode)
        Me.Controls.Add(Me.lbBarCode)
        Me.Controls.Add(Me.button1)
        Me.Controls.Add(Me.txtBarcode)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents btPrint As Button
    Private WithEvents lbCode As Label
    Private WithEvents lbBarCode As Label
    Private WithEvents button1 As Button
    Private WithEvents txtBarcode As TextBox
    Friend WithEvents PrintDialog2 As PrintDialog
    Friend WithEvents PrintDocument2 As Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog2 As PrintPreviewDialog
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents reportDocument1 As CrystalDecisions.CrystalReports.Engine.ReportDocument
End Class
