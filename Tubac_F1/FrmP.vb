Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient
Public Class FrmP
    Public con As New Conexion
    Dim objectCode As String
    Dim itemcode As String
    Dim oCompany As SAPbobsCOM.Company
    Dim connectionString As String = Conexion.ObtenerConexion.ConnectionString
    Public Shared PO As SAPbobsCOM.Documents
    Public Shared GoodsReceiptPO As SAPbobsCOM.Documents
    Public Shared SQL_Conexion As SqlConnection = New SqlConnection()

    Private Const CP_NOCLOSE_BUTTON As Integer = &H200
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get
    End Property
    Public Sub New(ByVal user As String)
        MyBase.New()
        InitializeComponent()
        '  Note which form has called this one
        ToolStripStatusLabel1.Text = user
    End Sub
    Private Sub FrmFase1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Select()
        cargaORDER()
    End Sub
    Public Function cargaORDER()
        Dim SQL_da As SqlDataAdapter = New SqlDataAdapter("SELECT T0.DocNum FROM OPOR T0 WHERE T0.DocType ='I' and  T0.CANCELED = 'N' and  T0.DocStatus ='O'", con.ObtenerConexion())
        Dim DT_dat As System.Data.DataTable = New System.Data.DataTable()
        SQL_da.Fill(DT_dat)
        DGV.DataSource = DT_dat
        con.ObtenerConexion.Close()
    End Function



    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Dim SQL_da As SqlDataAdapter = New SqlDataAdapter("SELECT T0.DocNum FROM OPOR T0 WHERE T0.DocType ='I' and  T0.CANCELED = 'N' and  T0.DocStatus ='O' and T0.DocNum LIKE '" + TextBox2.Text + "%' ORDER BY T0.DocNum", con.ObtenerConexion())
        Dim DT_dat As System.Data.DataTable = New System.Data.DataTable()
        SQL_da.Fill(DT_dat)
        DGV.DataSource = DT_dat
        con.ObtenerConexion.Close()
    End Sub

    Private Sub generaEntrada()
        Dim iResult As Integer = -1
        Dim iResult2 As Integer = -1
        Dim sResult As String = String.Empty
        Dim sOutput As String = String.Empty
        Dim sql As String
        Dim oRecordSet As SAPbobsCOM.Recordset
        Dim sql2 As String
        Dim oRecordSet2 As SAPbobsCOM.Recordset
        Dim docentry As String

        Try
            '------------------OBTIENE DOCENTRY------------
            sql = ("SELECT T0.DocEntry FROM OPOR T0 WHERE T0.DocNum = '" + txtOrder.Text + "'")
            oRecordSet = con.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet.DoQuery(sql)
            If oRecordSet.RecordCount > 0 Then
                docentry = oRecordSet.Fields.Item(0).Value
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
            oRecordSet = Nothing
            GC.Collect()

            PO = con.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseOrders)
            GoodsReceiptPO = con.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes)

            '----------------------------------------------
            PO.GetByKey(docentry)
            '----------------------------------------------

            GoodsReceiptPO.CardCode = PO.CardCode
            GoodsReceiptPO.CardName = PO.CardName
            '----------- LINEAS -----------------------------

            Dim itemcode As String
            Dim quantity As Double
            Dim i As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn()
            Dim existe As Boolean = DGV2.Columns.Cast(Of DataGridViewColumn).Any(Function(x) x.Name = "CHK")
            If existe = False Then
                DGV2.Columns.Add(i)
                i.HeaderText = "CHK"
                i.Name = "CHK"
                i.Width = 32
                i.DisplayIndex = 0
            End If
            Dim result As Integer = MessageBox.Show("Desea Ingresar la Orden?", "Atencion", MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Cancel Then
                MessageBox.Show("Cancelado")
            ElseIf result = DialogResult.No Then
                MessageBox.Show("No se realizara la orden")
            ElseIf result = DialogResult.Yes Then
                For Each row As DataGridViewRow In DGV2.Rows
                    Dim chk As DataGridViewCheckBoxCell = row.Cells("CHK")
                    If chk.Value IsNot Nothing AndAlso chk.Value = True Then

                        PO.Lines.SetCurrentLine(DGV2.Rows(chk.RowIndex).Cells.Item(3).Value.ToString)
                        PO.Lines.LineStatus = SAPbobsCOM.BoStatus.bost_Close
                        'GoodsReceiptPO.Lines.ItemCode = DGV2.Rows(chk.RowIndex).Cells.Item(1).Value.ToString
                        'GoodsReceiptPO.Lines.ItemDescription = PO.Lines.ItemDescription
                        GoodsReceiptPO.Lines.Quantity = DGV2.Rows(chk.RowIndex).Cells.Item(2).Value.ToString
                        GoodsReceiptPO.Lines.BaseEntry = PO.DocEntry
                        GoodsReceiptPO.Lines.BaseLine = DGV2.Rows(chk.RowIndex).Cells.Item(3).Value.ToString
                        GoodsReceiptPO.Lines.BaseType = Convert.ToInt32(PO.DocObjectCodeEx)

                        GoodsReceiptPO.Lines.BatchNumbers.SetCurrentLine(0)
                        GoodsReceiptPO.Lines.BatchNumbers.BatchNumber = "000-00" & DGV2.Rows(chk.RowIndex).Cells.Item(1).Value.ToString
                        GoodsReceiptPO.Lines.BatchNumbers.Quantity = Convert.ToDouble(DGV2.Rows(chk.RowIndex).Cells.Item(2).Value.ToString)
                        GoodsReceiptPO.Lines.BatchNumbers.AddmisionDate = Now
                        GoodsReceiptPO.Lines.BatchNumbers.Add()


                        GoodsReceiptPO.Lines.Add()
                    End If
                Next

            End If

            '-------------------------------BATCH--------------------------------------

            '---------------------------------------------------------------------------

            GoodsReceiptPO.Comments = PO.DocEntry
            iResult = GoodsReceiptPO.Add()
            If iResult <> 0 Then
                MessageBox.Show(con.oCompany.GetLastErrorDescription)
            Else
                PO.Comments = PO.DocEntry
                iResult2 = PO.Update()
                If iResult2 <> 0 Then
                    MessageBox.Show(con.oCompany.GetLastErrorDescription)
                End If
                MessageBox.Show("listo")
            End If
            con.oCompany.Disconnect()
        Catch ex As Exception
            MsgBox("Error: " + ex.Message.ToString)
            con.oCompany.Disconnect()
        End Try
    End Sub
    Private Sub GR_from_PO()
        Try
            If con.MakeConnectionSAP() Then
                generaEntrada()
            Else
                con.MakeConnectionSAP()
                If con.Connected Then
                    generaEntrada()
                Else
                    MessageBox.Show("Error de Conexion, intente Nuevamente")
                End If
            End If
        Catch ex As Exception
            MsgBox("Error: " + ex.Message.ToString)
        End Try
    End Sub

    Private Sub DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellContentClick
        txtOrder.Text = DGV(0, DGV.CurrentCell.RowIndex).Value.ToString()
    End Sub

    Private Sub txtOrder_TextChanged(sender As Object, e As EventArgs) Handles txtOrder.TextChanged
        Dim i As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn()
        Dim existe As Boolean = DGV2.Columns.Cast(Of DataGridViewColumn).Any(Function(x) x.Name = "CHK")
        If existe = False Then
            DGV2.Columns.Add(i)
            i.HeaderText = "CHK"
            i.Name = "CHK"
            i.Width = 32
            i.DisplayIndex = 0
        End If

        Dim SQL_da As SqlDataAdapter = New SqlDataAdapter("SELECT T0.[ItemCode], T0.[Quantity], isnull(T0.LineNum,0) FROM POR1 T0 WHERE T0.[LineStatus] = 'O' and T0.[DocEntry] like '" + txtOrder.Text + "%'", con.ObtenerConexion())
        Dim DT_dat As System.Data.DataTable = New System.Data.DataTable()
        SQL_da.Fill(DT_dat)
        DGV2.DataSource = DT_dat
        con.ObtenerConexion.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GR_from_PO()
        DGV.DataSource = Nothing
        DGV2.DataSource = Nothing
        TextBox2.Clear()
        txtOrder.Clear()
    End Sub

    Private Sub btnFinalizar_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As Integer = MessageBox.Show("Desea limpiar el objeto?", "Atencion", MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Cancel Then
            MessageBox.Show("Cancelado")
        ElseIf result = DialogResult.No Then
            MessageBox.Show("Puede continuar!")
        ElseIf result = DialogResult.Yes Then
            TextBox2.Clear()
            txtOrder.Clear()
            PO = Nothing
            GoodsReceiptPO = Nothing
            DGV.DataSource = Nothing
            DGV2.DataSource = Nothing
            MessageBox.Show("Inicie un objeto nuevo")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim result As Integer = MessageBox.Show("Desea salir del modulo?", "Atencion", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then
            MessageBox.Show("Puede continuar")
        ElseIf result = DialogResult.Yes Then
            MessageBox.Show("Finalizando modulo")
            Try
                con.oCompany.Disconnect()
            Catch
            End Try
            Application.Exit()
            Me.Close()
        End If
    End Sub
End Class
