Imports Microsoft.VisualBasic
Imports System.Web
Imports System.IO
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging



Public Class SaydanClass

#Region "Database Okuma/Yazma İşlemleri"
	''' <summary>Hangi veri tabanına bağlanılacağını belirler</summary>
	''' <returns>ilgili veri tabanına ait connectionstring değerini string formatında döndürür</returns>
	Public Function _DefaultConnectionStringDoldur() As String

		If System.Web.HttpContext.Current.Cache("DefConnStr") = Nothing Then
            Dim vbconadresi As String = "~/App_Code/DBDef.config"
            Dim str As StreamReader
            Dim satir As String
            str = New StreamReader(System.Web.HttpContext.Current.Server.MapPath(vbconadresi), System.Text.Encoding.Default)
            satir = str.ReadLine
            str.Close()
            'System.Web.HttpContext.Current.Cache("DefConnStr") = "Data Source=.;Initial Catalog=ETIC_MersinBorsasi;Persist Security Info=True;User ID=ETIC_USER;Password=Saydan;"
            System.Web.HttpContext.Current.Cache("DefConnStr") = satir
		End If
		Return System.Web.HttpContext.Current.Cache("DefConnStr")
	End Function

	''' <summary>Veri Tabanından kayıt çekmek için kullanılır</summary>
	''' <param name="CmdStr">Alanın ekleneceği TableRow</param>
	''' <returns>eğer cmdstr doğru verilmişse , uygun tablo verisi datatable formatında geri döner</returns>
	Public Function _ConnDB(ByVal CmdStr As String) As DataTable
		Dim ds As New DataSet
		Dim dt As DataTable
		Dim cn As New SqlConnection(_DefaultConnectionStringDoldur)
		Dim da As New SqlDataAdapter(CmdStr, cn)
		da.SelectCommand.CommandTimeout = "3000000"
		da.Fill(ds, "table")
		dt = ds.Tables("table")
		Return dt
	End Function

	''' <summary>Sadece Tablo adı girilen bir tabloya bağlanma</summary>
	''' <param name="TableName">Tablo adı</param>
	''' <returns>eğer Tablo Adı doğru girilmişse , uygun tablo verisi datatable formatında geri döner</returns>
	Public Function _ConnTbl(ByVal TableName As String, Optional WhereCondition As String = "", Optional SortCondition As String = "") As DataTable
		Dim ds As New DataSet
		Dim dt As DataTable
		Dim cn As New SqlConnection(_DefaultConnectionStringDoldur)

		Dim whrcnd As String = ""



		If WhereCondition <> "" Then
			WhereCondition = Trim(WhereCondition)
			If InStr(WhereCondition, " ") > 0 Then
				If Mid(LCase(WhereCondition), 1, InStr(WhereCondition, " ") - 1) = "and" Then
					whrcnd = Mid(WhereCondition, 5)
				Else
					whrcnd = WhereCondition
				End If
			Else
				whrcnd = WhereCondition
			End If
			whrcnd = "where " + whrcnd
		End If

		If SortCondition <> "" Then whrcnd += " order by " + SortCondition

		Dim da As New SqlDataAdapter("select * from " + TableName + " " + whrcnd, cn)
		da.SelectCommand.CommandTimeout = "3000000"
		da.Fill(ds, "table")
		dt = ds.Tables("table")
		Return dt
	End Function
	Public Function ExecSQL(ByVal cmdstr As String, Optional ByVal DBName As String = "DefaultVeriTabanı") As String
		Dim cn As New SqlConnection(_DefaultConnectionStringDoldur)
		Try
			Dim cm As New SqlCommand(cmdstr, cn)
			cm.CommandTimeout = "3000000"
			cn.Open()
			cm.ExecuteNonQuery()
			cn.Close()
		Catch ex As Exception
			Return ex.Message
		End Try
		Return "Kayıt Tamam"
	End Function
	Public Function _SaveDB(ByVal CmdStr As String, ByVal DataTable As DataTable, Optional ByVal _KayitTarihi As String = "") As String
		Try
			Dim ds As New DataSet
			Dim cn As New SqlConnection(_DefaultConnectionStringDoldur)
			Dim da As New SqlDataAdapter(CmdStr, cn)
			Dim cb As New SqlCommandBuilder(da)
			da.Update(DataTable)
			cn.Close()
			Return "OK"
		Catch ex As Exception
			Return ex.Message
		End Try
	End Function

#End Region

#Region "Detay Sayfası İşlemleri"

	Public Function DetayTabloTanimla(ByVal TabloAdi As String, ByVal PriKeyFieldName As String, ByVal PrikeyValue As String) As DataTable

		Dim dt As New DataTable
		dt.Columns.Add("TableName", System.Type.GetType("System.String"))
		dt.Columns.Add("PriKeyFieldName", System.Type.GetType("System.String"))
		dt.Columns.Add("IsNewRecord", System.Type.GetType("System.Boolean"))
		dt.Columns.Add("PrikeyValue", System.Type.GetType("System.String"))


		dt.Columns.Add("FieldName", System.Type.GetType("System.String"))
		dt.Columns.Add("FieldDataType", System.Type.GetType("System.String"))
		dt.Columns.Add("FieldOldValue", System.Type.GetType("System.String"))
		dt.Columns.Add("FieldNewValue", System.Type.GetType("System.String"))

		dt.Columns.Add("ControlName", System.Type.GetType("System.String"))
		dt.Columns.Add("ControlType", System.Type.GetType("System.String"))
		dt.Columns.Add("SecurityCode", System.Type.GetType("System.Int32"))

		Dim dr As DataRow = dt.NewRow
		dr("TableName") = TabloAdi
		dr("PriKeyFieldName") = PriKeyFieldName
		If PrikeyValue = -1 Then
			dr("IsNewRecord") = True
		Else
			dr("PrikeyValue") = PrikeyValue
			dr("IsNewRecord") = False
		End If
		dt.Rows.Add(dr)

		Return dt

	End Function

	Public Sub DetayTabloyaAlanEkle(ByVal dt As DataTable, ByVal FieldName As String, ByVal ControlName As String, ByVal ControlType As String, Optional ByVal FieldDataType As String = "String")
		'Bu isimle bir alan daha önce tabloya eklenmiş mi kontrol et
		Dim dr As DataRow = dt.NewRow
		dr("FieldName") = FieldName
		dr("FieldDataType") = FieldDataType
		dr("ControlName") = ControlName
		dr("ControlType") = ControlType
		dt.Rows.Add(dr)
	End Sub

	Public Function MaxPriKeyDegeriniBul(ByVal dt As DataTable) As Integer
		Dim dt1 As DataTable = _ConnDB("select max(" + dt.Rows(0)("PrikeyFieldName").ToString + ") from " + dt.Rows(0)("TableName").ToString)
		If dt1.Rows.Count = 0 Then
			Return 1
		Else
			Return dt1.Rows(0)(0) + 1
		End If
	End Function

	Public Function MaxPriKeyDegeriniBul(ByVal TableName As String, ByVal PriKeyFieldName As String) As Integer
		Dim dt1 As DataTable = _ConnDB("select max(" + PriKeyFieldName + ") from " + TableName)
		If dt1.Rows.Count = 0 Then
			Return 1
		ElseIf IsDBNull(dt1.Rows(0)(0)) Then
			Return 1
		Else
			Return dt1.Rows(0)(0) + 1
		End If
	End Function

	Public Sub DetayTabloKayitStringineEkle(ByVal dt As DataTable, ByVal Fieldname As String, ByVal Value As String)
		Dim dv As DataView = dt.DefaultView
		dv.RowFilter = "FieldName='" + Fieldname + "'"
		dv(0)("FieldNewValue") = Value
	End Sub



	Public Function DetayTabloyuKaydet(ByVal dt As DataTable) As String
		Dim _KayitTarihi As String = Now.ToString("dd.MM.yyyy HH:mm:ss.ffff ") + CInt(Rnd(100000) * 100000).ToString
		Dim cmdstr As String
		Dim mainform As System.Web.UI.Page = System.Web.HttpContext.Current.Handler
		Dim MaxPriKeyValue As Integer = 0
		If dt.Rows(0)("IsNewRecord") Then

			cmdstr = "insert into " + dt.Rows(0)("TableName").ToString
			Dim cmdFld As String = ""
			Dim cmdValue As String = ""
			Dim SiraNo As Integer = 0
			'MaxPriKeyValue = MaxPriKeyDegeriniBul(dt)

			cmdFld += "_KT"
			cmdValue += "'" + _KayitTarihi + "'"

			For SiraNo = 1 To dt.Rows.Count - 1
				Dim dr As DataRow = dt.Rows(SiraNo)
				If IsDBNull(dr("FieldNewValue")) Then Continue For
				cmdFld += ", " + dr("FieldName").ToString

				Dim Deger As String

				Deger = dr("FieldNewValue")

				Select Case dr("FieldDataType")
					Case "Date"
						If IsDate(Deger) Then
							LogaYaz("Deger : " + Deger)
							cmdValue += ", " + DateStr(Deger)
						Else
							LogaYaz("Deger : NULL")
							cmdValue += ", NULL"
						End If
					Case "Integer"
						If IsNumeric(Deger) Then
							cmdValue += ", " + Deger
						Else
							cmdValue += ", NULL"
						End If
					Case "Decimal"
						If IsNumeric(Deger) Then
							cmdValue += ", " + Deger
						Else
							cmdValue += ", NULL"
						End If
					Case "Boolean"
						Select Case Deger
							Case "Var", "Evet", "True", "1", "Yes"
								cmdValue += ",1"
							Case "Yok", "Hayır", "False", "0", "No"
								cmdValue += ",0"
							Case Else
								cmdValue += ",NULL"
						End Select
					Case Else
						cmdValue += ", '" + Deger + "'"
				End Select
			Next
			'cmdFld = Mid(cmdFld, 3)
			'cmdValue = Mid(cmdValue, 3)
			cmdstr += " (" + cmdFld + ") Values(" + cmdValue + ")"
		Else
			Dim dr As DataRow = dt.Rows(0)
			cmdstr = "UPDATE " + dr("TableName").ToString + " SET "
			Dim cmdFld As String = ""
			Dim cmdWhr As String = " where " + dr("PrikeyFieldName").ToString + " = " + dr("PrikeyValue").ToString
			Dim SiraNo As Integer = 0
			For SiraNo = 1 To dt.Rows.Count - 1

				dr = dt.Rows(SiraNo)

				If IsDBNull(dr("FieldNewValue")) Then Continue For

				Dim Deger As String

				Deger = dr("FieldNewValue").ToString

				cmdFld += ", " + dr("FieldName").ToString + "= "
				Select Case dr("FieldDataType").ToString
					Case "String"
						cmdFld += "'" + Deger + "'"
					Case "Date"
						If IsDate(Deger) Then
							cmdFld += DateStr(Deger)
						Else
							cmdFld += " NULL"
						End If
					Case "Integer"
						If IsNumeric(Deger) Then
							cmdFld += Deger
						Else
							cmdFld += " NULL"
						End If
					Case "Decimal"
						If IsNumeric(Deger) Then
							cmdFld += Deger
						Else
							cmdFld += " NULL"
						End If
					Case "Boolean"
						Select Case Deger
							Case "Var", "Evet", "True", "1", "Yes"
								cmdFld += "1"
							Case "Yok", "Hayır", "False", "0", "No"
								cmdFld += "0"
							Case Else
								cmdFld += "NULL"
						End Select
				End Select
			Next

			cmdstr += Mid(cmdFld, 3) + cmdWhr
		End If

		LogaYaz(cmdstr)

		Dim a As String = ExecSQL(cmdstr)
		If a = "Kayıt Tamam" Then
			If dt.Rows(0)("IsNewRecord") Then
				Dim dr As DataRow = dt.Rows(0)
				Dim PrimaryKey As Integer = _ConnDB("select " + dr("PriKeyFieldName").ToString + " from " + dr("TableName").ToString + " where _KT='" + _KayitTarihi + "'").Rows(0)(0)
				Return PrimaryKey.ToString
			Else
				Return dt.Rows(0)("PriKeyValue").ToString
			End If
		Else
			Return "Kayıt Sırasında Hata : " + a
		End If

	End Function



	Public Sub LogaYaz(ByVal ifade As String)
		'Dim stw As New StreamWriter(System.Web.HttpContext.Current.Server.MapPath("../Docs/sqlCommand.txt"), True, System.Text.Encoding.Default)
		'stw.Write(vbCrLf + "********************" + Now.ToString + vbCrLf + ifade)
		'stw.Close()

	End Sub

	Public Function DateStr(ByVal myDate As Date) As String
		Return "Convert(datetime,'" + myDate.ToString("dd.MM.yyyy HH:mm:ss") + "',103)"
		'Return "'" & Year(myDate) & "-" & Month(myDate) & "-" & Day(myDate) & " " & Hour(myDate) & ":" & Minute(myDate) & ":" & Second(myDate) & "'"
	End Function

	'Public Function DilTablosunuGetir() As DataView
	'    Dim mainform As System.Web.UI.Page = System.Web.HttpContext.Current.Handler
	'    DilTablosunuGetir = _ConnDB("SELECT * FROM PUB.web_Lang WHERE PageName= '" + mainform.AppRelativeVirtualPath + "'").DefaultView
	'End Function

	'Public Function GuvenlikDegerleriniGetir(ByVal MenuKodu As Integer) As String
	'    Dim dt As DataTable = _ConnDB("SELECT menuProgram FROM PUB.userMenu userMenu WHERE userMenu.menuKodu=" + MenuKodu.ToString + " AND userMenu.moduser_id='" + System.Web.HttpContext.Current.Session("UserID").ToString + "'")
	'    If dt.Rows.Count > 0 Then
	'        GuvenlikDegerleriniGetir = dt.Rows(0)(0)
	'    Else
	'        Return ""
	'    End If
	'End Function
#End Region

#Region "Liste Sayfası İşlemleri"
	''' <summary>Liste Tablosunda tanımlanmış bir satır'a hücre eklemek için bu prosedür kullanılır</summary>
	''' <param name="TableRowName">Alanın ekleneceği TableRow</param>
	''' <param name="Text">Alana girilecek bilgi</param>
	''' <param name="IsHeader">bu tablo başlığıysa true işaretleyin, değilse false</param>
	Public Sub _LstFldAdd(ByVal TableRowName As TableRow, ByVal Text As String, Optional ByVal IsHeader As Boolean = False, Optional ByVal CellClass As String = "", Optional ByVal cellwidth As String = "")
		If IsHeader = True Then
			Dim hc As New TableHeaderCell
			If CellClass <> "" Then hc.Attributes.Add("class", CellClass)
			If cellwidth <> "" Then hc.Style.Add(HtmlTextWriterStyle.Width, cellwidth)
			hc.Text = Text
			TableRowName.Cells.Add(hc)
		Else
			Dim hc As New TableCell
			If CellClass <> "" Then hc.Attributes.Add("class", CellClass)
			hc.Text = Text
			TableRowName.Cells.Add(hc)
		End If
	End Sub
	Public Sub LinkPage(ByVal Hr As TableRow, ByVal URL As String, Optional ByVal Target As String = "")
		If Target <> "" Then
			Hr.Attributes.Add("onclick", "window.open('" + URL + "', '" + Target + "') ;")
		Else
			Hr.Attributes.Add("onclick", "window.location.href = '" + URL + "';")
		End If
		Hr.Style.Add("cursor", "pointer")
	End Sub

	Public Sub LinkPage(ByVal Hc As TableCell, ByVal URL As String, Optional ByVal Target As String = "")
		If Target <> "" Then
			Hc.Attributes.Add("onclick", "window.open('" + URL + "', '" + Target + "') ;")
		Else
			Hc.Attributes.Add("onclick", "window.location.href = '" + URL + "';")
		End If
		Hc.Style.Add("cursor", "pointer")
	End Sub

#End Region

#Region "Hiyerarşik Tablo Oluşturma"
	Public Function Hiyerarsik_Tablo_Olusturma(ByVal dt As DataTable, ByVal PrimaryKeyFieldName As String, ByVal ParentFieldName As String, Optional ByVal StartingPrimaryKey As Integer = 0, Optional ByVal ExceptedPrimaryKey As Integer = 0) As DataTable
		Dim YeniTablo As New DataTable
		For Each dc As DataColumn In dt.Columns
			YeniTablo.Columns.Add(dc.ColumnName, dc.DataType)
		Next
		YeniTablo.Columns.Add("__Level", System.Type.GetType("System.Int32"))

		dt.Columns.Add("IslemOK", System.Type.GetType("System.Boolean"))
		Dim dv As DataView = dt.DefaultView
		For Each dr As DataRow In dt.Rows
			If IsDBNull(dr(ParentFieldName)) Then
				'parenti boşsa da sıfır yap
				dr(ParentFieldName) = 0
				'ElseIf dr(ParentFieldName) = -1 And dr(PrimaryKeyFieldName) = -1 Then
				'eğer Parent'ı -1 yapılmışsa personelin bu kaydı görmeye yetkisi yok demektir. listeden çıkar.
				'dr("IslemOK") = True
			Else
				dv.RowFilter = PrimaryKeyFieldName + "=" + dr(ParentFieldName).ToString
				If dv.Count = 0 Then
					'Parenti tabloda yoksa sıfır yap
					dr(ParentFieldName) = 0
				Else
					'YANLIŞLIKLA KENDİNE BAĞLANMIŞSA ONU DA SIFIR YAP
					If dr(ParentFieldName) = dr(PrimaryKeyFieldName) Then dr(ParentFieldName) = 0
				End If
			End If
		Next
		Dim SecilenPrimaryKey As Integer = StartingPrimaryKey
		Dim Level As Integer = 0
		Dim GeriMiDondu As Boolean = False
		'SecilenPrimaryKey'i yaz
		'SecilenPrimaryKey'in çocuğu Var mı?
		'   Varsa yukarıya dön
		'   Yoksa yeni SeçilenPrimaryKey önceki seçilenprimarykey'in ParentPrimaryKey'i seç, Yukarıya dön
		Do
			If Not GeriMiDondu Then
				If SecilenPrimaryKey > 0 Then
					dv.RowFilter = "IslemOK is null and " + PrimaryKeyFieldName + "=" + SecilenPrimaryKey.ToString
					If dv.Count = 0 Then
						If Not IsDBNull(dv(0)(ParentFieldName)) Then
							SecilenPrimaryKey = dv(0)(ParentFieldName)
						Else
							SecilenPrimaryKey = 0
						End If
						Level -= 1
						GeriMiDondu = True
						Continue Do
					End If
					Dim drv As DataRowView = dv(0)
					Dim YeniTabloRow As DataRow = YeniTablo.NewRow
					For Each dc As DataColumn In dt.Columns
						If dc.ColumnName <> "IslemOK" Then YeniTabloRow(dc.ColumnName) = drv(dc.ColumnName)
					Next
					YeniTabloRow("__Level") = Level
					YeniTablo.Rows.Add(YeniTabloRow)
					drv("IslemOK") = True
				End If
			End If
			If SecilenPrimaryKey = 0 Then
				dv.RowFilter = "IslemOK is null and (" + ParentFieldName + "=" + SecilenPrimaryKey.ToString + " OR " + ParentFieldName + " is null)"
			Else
				dv.RowFilter = "IslemOK is null and " + ParentFieldName + "=" + SecilenPrimaryKey.ToString
			End If
			If dv.Count > 0 Then
				SecilenPrimaryKey = dv(0)(PrimaryKeyFieldName)
				If SecilenPrimaryKey = ExceptedPrimaryKey Then
					dv.RowFilter = PrimaryKeyFieldName + "=" + SecilenPrimaryKey.ToString
					If Not IsDBNull(dv(0)(ParentFieldName)) Then
						SecilenPrimaryKey = dv(0)(ParentFieldName)
					Else
						SecilenPrimaryKey = 0
					End If
					Level -= 1
					GeriMiDondu = True
					Continue Do
				End If
				Level += 1
				GeriMiDondu = False
				Continue Do
			Else
				If SecilenPrimaryKey = StartingPrimaryKey Then Exit Do
				dv.RowFilter = PrimaryKeyFieldName + "=" + SecilenPrimaryKey.ToString
				If Not IsDBNull(dv(0)(ParentFieldName)) Then
					SecilenPrimaryKey = dv(0)(ParentFieldName)
				Else
					SecilenPrimaryKey = 0
				End If
				Level -= 1
				GeriMiDondu = True
				Continue Do
			End If
		Loop


		Hiyerarsik_Tablo_Olusturma = YeniTablo
	End Function
	Public Function AltKayitlariniBul(ByVal dt As DataTable, ByVal PrimaryKeyFieldName As String, ByVal ParentFieldName As String, ByVal StartingPrimaryKey As Integer, Optional ByVal KendiDahilMi As Boolean = True) As DataTable
		Dim YeniTablo As New DataTable
		YeniTablo.Columns.Add("PrimaryKey", System.Type.GetType("System.Int32"))
		Dim dv As DataView = dt.DefaultView
		Dim drYeniTablo As DataRow
		Dim selectedNumber As Integer = -1
		Dim recordcount As Integer = 0
		If KendiDahilMi = True Then
			drYeniTablo = YeniTablo.NewRow
			drYeniTablo(0) = StartingPrimaryKey
			YeniTablo.Rows.Add(drYeniTablo)
			selectedNumber += 1
			recordcount += 1
		End If
		Do Until selectedNumber = recordcount
			If selectedNumber = -1 Then
				dv.RowFilter = ParentFieldName + "=" + StartingPrimaryKey.ToString
			Else
				dv.RowFilter = ParentFieldName + "=" + YeniTablo.Rows(selectedNumber)(0).ToString
			End If
			For Each drv As DataRowView In dv
				drYeniTablo = YeniTablo.NewRow
				drYeniTablo(0) = drv(PrimaryKeyFieldName)
				YeniTablo.Rows.Add(drYeniTablo)
				recordcount += 1
			Next
			selectedNumber += 1
		Loop


		AltKayitlariniBul = YeniTablo
	End Function

	''' <summary>Bir Dropdownlist kontrolünün içeriğini veri tabanındaki bir tablo içeriğinden doldurur</summary>
	''' <param name="drop">doldurulacak dropdownlist'in adı</param>
	''' <param name="TableName">veriyi içeren tablonun adı</param>
	''' <param name="ValueField">Dropdownlist'in value hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="ParentValueField">Dropdownlist'in Hiyerarşisini belirleyen alan adı (Ebeveyn)</param>
	''' <param name="TextField">Dropdownlist'in text hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="SecinizVarMi">Dropdownlist'in text hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="TumuVarMi">Dropdownlist'in text hanesinin doldurulacağı tablodaki alan adı</param>
	Public Sub _HDrpDoldur(ByVal drop As DropDownList, ByVal TableName As String, ByVal ValueField As String, ByVal ParentValueField As String, ByVal TextField As String, Optional ByVal SecinizVarMi As Boolean = False, Optional ByVal TumuVarMi As Boolean = False, Optional ByVal WhereCondition As String = "", Optional ByVal SortCondition As String = "")

		Dim dt As DataTable = _ConnTbl(TableName, WhereCondition, SortCondition)
		Dim HTbl As DataTable = Hiyerarsik_Tablo_Olusturma(dt, ValueField, ParentValueField)

		For Each dr As DataRow In HTbl.Rows
			Dim li As New ListItem
			li.Value = dr(ValueField)
			Dim girinti As String = ""
			If dr("__Level") > 1 Then
				For i = 1 To CInt(dr("__Level"))
					girinti += "&nbsp;&nbsp;&nbsp;&nbsp;"
				Next
			End If

			Dim writer As New System.IO.StringWriter()
			System.Web.HttpContext.Current.Server.HtmlDecode(girinti, writer)

			girinti = writer.ToString
			li.Text = girinti + dr(TextField).ToString
			drop.Items.Add(li)
		Next
		If TumuVarMi = True Then drop.Items.Insert(0, "Tümü")
		If SecinizVarMi = True Then drop.Items.Insert(0, "Seçiniz")
	End Sub
	Public Sub _HDrpDoldur(ByVal drop As CheckBoxList, ByVal TableName As String, ByVal ValueField As String, ByVal ParentValueField As String, ByVal TextField As String, Optional ByVal SecinizVarMi As Boolean = False, Optional ByVal TumuVarMi As Boolean = False, Optional ByVal WhereCondition As String = "", Optional ByVal SortCondition As String = "")

		Dim dt As DataTable = _ConnTbl(TableName, WhereCondition, SortCondition)
		Dim HTbl As DataTable = Hiyerarsik_Tablo_Olusturma(dt, ValueField, ParentValueField)

		For Each dr As DataRow In HTbl.Rows
			Dim li As New ListItem
			li.Value = dr(ValueField)
			Dim girinti As String = ""
			If dr("__Level") > 1 Then
				For i = 1 To CInt(dr("__Level"))
					girinti += "&nbsp;&nbsp;&nbsp;&nbsp;"
				Next
			End If

			Dim writer As New System.IO.StringWriter()
			System.Web.HttpContext.Current.Server.HtmlDecode(girinti, writer)

			girinti = writer.ToString
			li.Text = girinti + dr(TextField).ToString
			drop.Items.Add(li)
		Next
		If TumuVarMi = True Then drop.Items.Insert(0, "Tümü")
		If SecinizVarMi = True Then drop.Items.Insert(0, "Seçiniz")
	End Sub


#End Region

#Region "DropDownList Doldurma İşlemleri"

	''' <summary>Bir Dropdownlist kontrolünün içeriğini veri tabanındaki bir tablo içeriğinden doldurur</summary>
	''' <param name="drop">doldurulacak dropdownlist'in adı</param>
	''' <param name="TableName">veriyi içeren tablonun adı</param>
	''' <param name="ValueField">Dropdownlist'in value hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="TextField">Dropdownlist'in text hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="SecinizVarMi">Dropdownlist'in text hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="TumuVarMi">Dropdownlist'in text hanesinin doldurulacağı tablodaki alan adı</param>
	Public Sub _DrpDoldur(ByVal drop As DropDownList, ByVal TableName As String, ByVal ValueField As String, ByVal TextField As String, Optional ByVal SecinizVarMi As Boolean = False, Optional ByVal TumuVarMi As Boolean = False, Optional ByVal WhereCondition As String = "", Optional ByVal SortCondition As String = "")
		Dim dv As DataView = _ConnTbl(TableName, WhereCondition, SortCondition).DefaultView
		If SortCondition <> "" Then
			dv.Sort = SortCondition
		Else
			dv.Sort = TextField
		End If
		drop.DataSource = dv
		drop.DataValueField = ValueField
		drop.DataTextField = TextField
		drop.DataBind()
		If TumuVarMi = True Then drop.Items.Insert(0, "Tümü")
		If SecinizVarMi = True Then drop.Items.Insert(0, "Seçiniz")
	End Sub

	''' <summary>Bir CheckBoxList kontrolünün içeriğini veri tabanındaki bir tablo içeriğinden doldurur</summary>
	''' <param name="drop">doldurulacak CheckBoxList'in adı</param>
	''' <param name="TableName">veriyi içeren tablonun adı</param>
	''' <param name="ValueField">CheckBoxList'in value hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="TextField">CheckBoxList'in text hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="SecinizVarMi">CheckBoxList'in text hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="TumuVarMi">CheckBoxList'in text hanesinin doldurulacağı tablodaki alan adı</param>
	Public Sub _DrpDoldur(ByVal drop As CheckBoxList, ByVal TableName As String, ByVal ValueField As String, ByVal TextField As String, Optional ByVal SecinizVarMi As Boolean = False, Optional ByVal TumuVarMi As Boolean = False)
		Dim dv As DataView = _ConnTbl(TableName).DefaultView
		dv.Sort = TextField
		drop.DataSource = dv
		drop.DataValueField = ValueField
		drop.DataTextField = TextField
		drop.DataBind()
		If TumuVarMi = True Then drop.Items.Insert(0, "Tümü")
		If SecinizVarMi = True Then drop.Items.Insert(0, "Seçiniz")
	End Sub
	''' <summary>Bir RadioButtonList kontrolünün içeriğini veri tabanındaki bir tablo içeriğinden doldurur</summary>
	''' <param name="drop">doldurulacak RadioButtonList'in adı</param>
	''' <param name="TableName">veriyi içeren tablonun adı</param>
	''' <param name="ValueField">RadioButtonList'in value hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="TextField">RadioButtonList'in text hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="SecinizVarMi">RadioButtonList'in text hanesinin doldurulacağı tablodaki alan adı</param>
	''' <param name="TumuVarMi">RadioButtonList'in text hanesinin doldurulacağı tablodaki alan adı</param>
	Public Sub _DrpDoldur(ByVal drop As RadioButtonList, ByVal TableName As String, ByVal ValueField As String, ByVal TextField As String, Optional ByVal SecinizVarMi As Boolean = False, Optional ByVal TumuVarMi As Boolean = False)
		Dim dv As DataView = _ConnTbl(TableName).DefaultView
		dv.Sort = TextField
		drop.DataSource = dv
		drop.DataValueField = ValueField
		drop.DataTextField = TextField
		drop.DataBind()
		If TumuVarMi = True Then drop.Items.Insert(0, "Tümü")
		If SecinizVarMi = True Then drop.Items.Insert(0, "Seçiniz")
	End Sub




	' ''' <summary>Bir Dropdownlist kontrolünün içeriğini veri tabanındaki bir tablo içeriğinden doldurur</summary>
	' ''' <param name="drop">doldurulacak dropdownlist'in adı</param>
	' ''' <param name="TypeName">doldurulacak yapıya ait Veri : </param>

	'Public Sub _AutoDrp(ByVal drop As DropDownList, ByVal TypeName As String)
	'    Select Case TypeName
	'        Case ""
	'    End Select
	'End Sub

#End Region

#Region "Münferit Prosedür ve Fonksiyonlar"

	Public Function SayfadaControlBul() As DataTable
		Dim mainform As System.Web.UI.Page = System.Web.HttpContext.Current.Handler
		Dim SonKod As Integer = -1
		Dim dt As New DataTable

		dt.CaseSensitive = False
		dt.Columns.Add("Kod", System.Type.GetType("System.Int32"))
		dt.Columns.Add("ParentKod", System.Type.GetType("System.Int32"))
		dt.Columns.Add("ControlID", System.Type.GetType("System.String"))
		dt.Columns.Add("UniqueID", System.Type.GetType("System.String"))
		dt.Columns.Add("ControlType", System.Type.GetType("System.String"))
		dt.Columns.Add("LCaseControlID", System.Type.GetType("System.String"))
		dt.Columns.Add("IslemOK", System.Type.GetType("System.Boolean"))

		Dim dtson As New DataTable
		dtson.Columns.Add("ControlID", System.Type.GetType("System.String"))
		dtson.Columns.Add("ControlType", System.Type.GetType("System.String"))


		For Each subcnt As Control In mainform.Form.Controls



			'If subcnt.ID = Nothing Then Continue For
			SonKod += 1
			Dim drYeni As DataRow = dt.NewRow
			drYeni("Kod") = SonKod
			drYeni("ParentKod") = 0
			If Not subcnt.ID Is Nothing Then
				drYeni("ControlID") = subcnt.ID
				drYeni("LCaseControlID") = LCase(subcnt.ID)
			Else
				drYeni("ControlID") = Convert.DBNull
				drYeni("LCaseControlID") = Convert.DBNull

				If Mid(subcnt.ID, 1, 1) = "U" And subcnt.GetType.Name.IndexOf("controls_u") > -1 Then
					Dim drson As DataRow = dtson.NewRow
					drson("ControlID") = subcnt.ID
					drson("ControlType") = subcnt.GetType.Name
					dtson.Rows.Add(drson)
				End If



			End If
			drYeni("UniqueID") = subcnt.UniqueID
			drYeni("ControlType") = subcnt.GetType.Name
			drYeni("IslemOK") = False
			dt.Rows.Add(drYeni)

		Next
		Dim IslemNo As Integer = -1
		Do
			Dim dv As DataView = dt.DefaultView
			dv.RowFilter = "IslemOK=0"
			If dv.Count = 0 Then Exit Do
			Dim dr As DataRowView = dv(0)
			Dim CtrIDStr As String = dr("UniqueID")
			Dim cnt As Control = mainform.Form.FindControl(CtrIDStr)
			If cnt.GetType.Name = "CheckBoxList" Or cnt.GetType.Name = "RadioButtonList" Then
				dr("IslemOK") = True
				Continue Do
			End If
			For Each subcnt As Control In cnt.Controls
				Dim drYeni As DataRow = dt.NewRow
				SonKod += 1
				If SonKod = 10000 Then
					Dim msgSubject As String = "Saydancls1.SayfadaControlBul prosedüründe Hata"
					Dim msgbody As String = mainform.Page.AppRelativeVirtualPath + " Sayfasının Yetki Kontrolleri sırasında, ilgili sayfanın kontrolleri sorgulanırken, kontrol sayısı 10000'i aştığı tespit edildi. Bu durum belli bir kontrolün loop oluşturmasından kaynaklanıyor olabilir. lütfen kontrol ediniz."
					'ProblemGoreviOlustur(msgSubject, msgbody)
					Exit For
				End If
				drYeni("Kod") = SonKod
				drYeni("ParentKod") = dr("Kod")
				If Not subcnt.ID Is Nothing Then
					drYeni("ControlID") = subcnt.ID
					drYeni("LCaseControlID") = LCase(subcnt.ID)

					If Mid(subcnt.ID, 1, 1) = "U" And subcnt.GetType.Name.IndexOf("controls_u") > -1 Then
						Dim drson As DataRow = dtson.NewRow
						drson("ControlID") = subcnt.ID
						drson("ControlType") = subcnt.GetType.Name
						dtson.Rows.Add(drson)
					End If
				Else
					drYeni("ControlID") = Convert.DBNull
					drYeni("LCaseControlID") = Convert.DBNull
				End If
				drYeni("UniqueID") = subcnt.UniqueID
				drYeni("ControlType") = subcnt.GetType.Name
				drYeni("IslemOK") = False
				dt.Rows.Add(drYeni)
			Next
			dr("IslemOK") = True
		Loop
		dt.Columns.Remove("IslemOK")
		Return dtson

	End Function

	Public Function SayfadaControlBul2() As DataTable
		Dim mainform As System.Web.UI.Page = System.Web.HttpContext.Current.Handler
		Dim SonKod As Integer = -1
		Dim dt As New DataTable

		dt.CaseSensitive = False
		dt.Columns.Add("Kod", System.Type.GetType("System.Int32"))
		dt.Columns.Add("ParentKod", System.Type.GetType("System.Int32"))
		dt.Columns.Add("ControlID", System.Type.GetType("System.String"))
		dt.Columns.Add("UniqueID", System.Type.GetType("System.String"))
		dt.Columns.Add("ControlType", System.Type.GetType("System.String"))
		dt.Columns.Add("LCaseControlID", System.Type.GetType("System.String"))
		dt.Columns.Add("IslemOK", System.Type.GetType("System.Boolean"))
		For Each subcnt As Control In mainform.Form.Controls

			'If subcnt.ID = Nothing Then Continue For
			SonKod += 1
			Dim drYeni As DataRow = dt.NewRow
			drYeni("Kod") = SonKod
			drYeni("ParentKod") = 0
			If Not subcnt.ID Is Nothing Then
				drYeni("ControlID") = subcnt.ID
				drYeni("LCaseControlID") = LCase(subcnt.ID)
			Else
				drYeni("ControlID") = Convert.DBNull
				drYeni("LCaseControlID") = Convert.DBNull
			End If
			drYeni("UniqueID") = subcnt.UniqueID
			drYeni("ControlType") = subcnt.GetType.Name
			drYeni("IslemOK") = False
			dt.Rows.Add(drYeni)

		Next
		Dim IslemNo As Integer = -1
		Do
			Dim dv As DataView = dt.DefaultView
			dv.RowFilter = "IslemOK=0"
			If dv.Count = 0 Then Exit Do
			Dim dr As DataRowView = dv(0)
			Dim CtrIDStr As String = dr("UniqueID")
			Dim cnt As Control = mainform.Form.FindControl(CtrIDStr)
			If cnt.GetType.Name = "CheckBoxList" Or cnt.GetType.Name = "RadioButtonList" Then
				dr("IslemOK") = True
				Continue Do
			End If
			For Each subcnt As Control In cnt.Controls
				Dim drYeni As DataRow = dt.NewRow
				SonKod += 1
				If SonKod = 10000 Then
					Dim msgSubject As String = "Saydancls1.SayfadaControlBul prosedüründe Hata"
					Dim msgbody As String = mainform.Page.AppRelativeVirtualPath + " Sayfasının Yetki Kontrolleri sırasında, ilgili sayfanın kontrolleri sorgulanırken, kontrol sayısı 10000'i aştığı tespit edildi. Bu durum belli bir kontrolün loop oluşturmasından kaynaklanıyor olabilir. lütfen kontrol ediniz."
					'ProblemGoreviOlustur(msgSubject, msgbody)
					Exit For
				End If
				drYeni("Kod") = SonKod
				drYeni("ParentKod") = dr("Kod")
				If Not subcnt.ID Is Nothing Then
					drYeni("ControlID") = subcnt.ID
					drYeni("LCaseControlID") = LCase(subcnt.ID)
				Else
					drYeni("ControlID") = Convert.DBNull
					drYeni("LCaseControlID") = Convert.DBNull
				End If
				drYeni("UniqueID") = subcnt.UniqueID
				drYeni("ControlType") = subcnt.GetType.Name
				drYeni("IslemOK") = False
				dt.Rows.Add(drYeni)
			Next
			dr("IslemOK") = True
		Loop
		dt.Columns.Remove("IslemOK")
		Return dt

	End Function


	Public Sub LogaKaydet()
		Dim stack As Stack
		Dim page = DirectCast(System.Web.HttpContext.Current.Handler, Page)

		If Not System.Web.HttpContext.Current.Session("Stack") Is Nothing Then
			stack = System.Web.HttpContext.Current.Session("Stack")
			If stack.Count = 0 Then
				stack.Push(page.AppRelativeVirtualPath + "?" + page.ClientQueryString)
			ElseIf page.AppRelativeVirtualPath + "?" + page.ClientQueryString <> stack.Peek Then
				stack.Push(page.AppRelativeVirtualPath + "?" + page.ClientQueryString)
			Else
				Return
			End If
		Else
			stack = New Stack
			stack.Push(page.AppRelativeVirtualPath + "?" + page.ClientQueryString)
			'Dim btngeri As Global.System.Web.UI.WebControls.LinkButton = page.Master.FindControl("btngeri")
			'btngeri.Enabled = False
		End If
		System.Web.HttpContext.Current.Session("stack") = stack

	End Sub

	'Public Sub HataMesaji(ByVal Text As String, Optional ByVal Baslik As String = "Hata Mesajı")
	'	Dim mainform As System.Web.UI.Page = System.Web.HttpContext.Current.Handler

	'	Dim UMessage As UMessage = mainform.Page.Master.FindControl("UMessage")
	'	UMessage.Deger = Text

	'End Sub

#End Region

	Public Function SayisalDegeri1Artir(ByVal İfade As String) As String		'İfade112
		If İfade = "" Then Return "1"
		Dim rakam As String = ""
		Dim DegerHanesi As Integer
		For i = Len(İfade) To 1 Step -1							'length : 8
			Dim tmp1 As String = Mid(İfade, i, 1)
			If Not IsNumeric(tmp1) Then
				DegerHanesi = i
				Exit For
			End If
			rakam = tmp1 + rakam
		Next
		Dim SonucDeger As String = ""
		If DegerHanesi > 2 Then SonucDeger = Mid(İfade, 1, DegerHanesi - 1)
		Return SonucDeger + (rakam + 1).ToString
	End Function

	Public Function MailGonder(ByVal MailTO As String, ByVal MailSubject As String, ByVal MailBody As String, Optional ByVal MailCC As String = "", Optional ByVal MailBCC As String = "", Optional ByVal MailAttachments As String = "", Optional ByVal MailFromText As String = "", Optional ByVal MailIsBodyHTML As Boolean = True) As String

		'Parametrelerden Mail Yetkilerini Al
		'Dim cmdstr As String = "select ParametreAdi,Deger from SY_PARAMETRELER where ParametreAdi like 'Mail%'"
		'Dim dv As DataView = _ConnDB(cmdstr).DefaultView
		'dv.RowFilter = "ParametreAdi='MailSunucusu'"
		'Dim _mailHost As String = dv(0)("Deger").ToString
		'dv.RowFilter = "ParametreAdi='MailGondermeAdresi'"
		'Dim _mailAdresi As String = dv(0)("Deger").ToString
		'dv.RowFilter = "ParametreAdi='MailGondermeAdresiSifresi'"
		'Dim _mailSifresi As String = dv(0)("Deger").ToString



		Dim _mailHost As String = "smtp.saydan.net"
		Dim _mailAdresi As String = "murat@saydan.net"
		Dim _mailSifresi As String = "Mersin2685"
		MailFromText = "www.MersinBorsasi.com"

		Dim mesaj As New MailMessage
		mesaj.From = New MailAddress(_mailAdresi, MailFromText)


		Dim tmp As String = MailTO
		Do
			If InStr(tmp, "@") = 0 Then Exit Do
			Dim postmp As Integer = InStr(tmp, ";")
			If postmp > 0 Then
				mesaj.To.Add(Mid(tmp, 1, postmp - 1))
				tmp = Mid(tmp, postmp + 1)
			Else
				mesaj.To.Add(tmp)
				tmp = ""
			End If
		Loop

		tmp = MailCC
		Do
			If InStr(tmp, "@") = 0 Then Exit Do
			Dim postmp As Integer = InStr(tmp, ";")
			If postmp > 0 Then
				mesaj.CC.Add(Mid(tmp, 1, postmp - 1))
				tmp = Mid(tmp, postmp + 1)
			Else
				mesaj.CC.Add(tmp)
				tmp = ""
			End If
		Loop

		tmp = MailBCC
		Do
			If InStr(tmp, "@") = 0 Then Exit Do
			Dim postmp As Integer = InStr(tmp, ";")
			If postmp > 0 Then
				mesaj.Bcc.Add(Mid(tmp, 1, postmp - 1))
				tmp = Mid(tmp, postmp + 1)
			Else
				mesaj.Bcc.Add(tmp)
				tmp = ""
			End If
		Loop

		tmp = MailAttachments
		Do
			If InStr(tmp, "@") = 0 Then Exit Do
			Dim postmp As Integer = InStr(tmp, ";")
			If postmp > 0 Then
				mesaj.Attachments.Add(New Attachment(System.Web.HttpContext.Current.Server.MapPath(Mid(tmp, 1, postmp - 1))))
				tmp = Mid(tmp, postmp + 1)
			Else
				mesaj.Attachments.Add(New Attachment(System.Web.HttpContext.Current.Server.MapPath(tmp)))
				tmp = ""
			End If
		Loop

		mesaj.Subject = MailSubject
		mesaj.Body = MailBody
		mesaj.IsBodyHtml = MailIsBodyHTML


		Dim smtp As New SmtpClient(_mailHost, 587)
		Dim smtpUserInfo As New System.Net.NetworkCredential(_mailAdresi, _mailSifresi)
		smtp.UseDefaultCredentials = True
		smtp.Credentials = smtpUserInfo
		smtp.Port = 587
		'smtp.Timeout = 400000
		smtp.Host = _mailHost

		Try
			smtp.Send(mesaj)
			Return "OK"
		Catch ex As Exception
			Return ex.Message
		End Try


	End Function

	Public Function ThumbnailImage(ByVal DosyaYolu As String, Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0) As String
		Dim DosyaKonumu As String = Mid(DosyaYolu, 1, InStrRev(DosyaYolu, "/"))
		Dim DosyaAdi As String = Mid(DosyaYolu, InStrRev(DosyaYolu, "/") + 1)
		Dim extension As String = Mid(DosyaAdi, InStrRev(DosyaAdi, ".") + 1)
		If Not File.Exists(System.Web.HttpContext.Current.Server.MapPath(DosyaKonumu + DosyaAdi)) Then
			Return ""
		End If

		Dim imgPhoto As System.Drawing.Image = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(DosyaKonumu + DosyaAdi))
		Dim destWidth As Integer
		Dim destHeight As Integer
		Dim sourceWidth As Integer = imgPhoto.Width
		Dim sourceHeight As Integer = imgPhoto.Height

		If Width = 0 And Height = 0 Then
			destWidth = 150
			destHeight = sourceHeight * destWidth / sourceWidth
		ElseIf Width > 0 And Height = 0 Then
			destWidth = Width
			destHeight = sourceHeight * destWidth / sourceWidth
		ElseIf Width = 0 And Height > 0 Then
			destHeight = Height
			destWidth = sourceWidth * Height / sourceHeight
		Else
			destWidth = Width
			destHeight = Height
		End If

		imgPhoto = imgPhoto.GetThumbnailImage(destWidth, destHeight, Nothing, IntPtr.Zero)
		Dim timestamp As String = Now.ToString("ddMMyyyyhhmmssffff") + CInt(Rnd(100000) * 100000).ToString
		timestamp = Replace(Replace(timestamp, ".", ""), ":", "")
		Dim TempDosyaAdi As String = timestamp + "." + extension
		imgPhoto.Save(System.Web.HttpContext.Current.Server.MapPath("~/Docs/temp/" + TempDosyaAdi), ImageFormat.Png) 'Ekrana basıyoruz..     
		imgPhoto.Dispose()
		Return "../Docs/temp/" + TempDosyaAdi

	End Function

	Public Sub LoginKontroluYap()
		If System.Web.HttpContext.Current.Session("UserID") Is Nothing Then
			Dim mainform As System.Web.UI.Page = System.Web.HttpContext.Current.Handler
			System.Web.HttpContext.Current.Session("LoginOncesiSayfa") = mainform.AppRelativeVirtualPath + "?" + mainform.ClientQueryString
			System.Web.HttpContext.Current.Response.Redirect("login.aspx")
		Else
			Exit Sub
		End If
	End Sub


	Public Function YoneticiMi() As Boolean

		If System.Web.HttpContext.Current.Session("YoneticiMi") Is Nothing Then Return False
		If System.Web.HttpContext.Current.Session("YoneticiMi") <> True Then Return False
		Return True
	End Function

End Class
