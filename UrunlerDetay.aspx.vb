Imports System.Data
Partial Class UrunlerDetay
	Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass
	Dim KontrolOK As Boolean = True
	Dim Kod As Integer = 0
	Dim UstKod As Integer = 0

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Kod = Request.QueryString("kod")
		UstKod = Request.QueryString("ustkod")

		'Kod = 28

		If Not IsPostBack Then
			Saydanclass._HDrpDoldur(chkKategoriler, "KATEGORILER", "KategoriKodu", "EbeveynKategoriKodu", "StokGrupAdi1")
			If Kod > 0 Then VarOlanKaydinBilgisiniDoldur(Kod)
		End If
		'If Kod = -1 Then
		'	spnUrunler.Visible = False
		'Else
		'	UrunlerListesi()
		'End If

	End Sub

	Protected Sub VarOlanKaydinBilgisiniDoldur(ByVal Ref As Integer)
		Dim cmdstr As String = "SELECT * FROM URUNLER where StokKodu=" + Ref.ToString
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim dr As DataRow = dt.Rows(0)
		btnSil.Visible = True
		lblUrunKodu.Text = dr("StokKodu").ToString
		txtUrunAdi.Text = dr("StokAdi1").ToString
		txtKisaAciklama.Text = dr("KisaAciklama1").ToString
		txtBarkodNo.Text = dr("BarkodNo").ToString
		txtMiktar.Text = dr("Miktar").ToString
		drpAktifMi.SelectedValue = dr("AktifMi").ToString
		txtAlisFiyati.Text = dr("AlisFiyati").ToString
		txtSatisFiyati.Text = dr("SatisFiyati").ToString
		txtTedarikciFirma.Text = dr("TedarikciFirma").ToString
		txtKeyword.Text = dr("Keywords").ToString
		txtDescription.Text = dr("Description").ToString
		txtUrunDetayi.Text = dr("DetayAciklama1").ToString
		lblDetay.Text = dr("DetayAciklama1").ToString
		txtResim1.Text = dr("Pic1").ToString
		txtResim2.Text = dr("Pic2").ToString
		txtResim3.Text = dr("Pic3").ToString
		txtResim4.Text = dr("Pic4").ToString
		txtResim5.Text = dr("Pic5").ToString


		cmdstr = "select KategoriKodu from KATEGORILER_URUNLER where UrunKodu=" + Ref.ToString
		Dim dv As DataView = Saydanclass._ConnDB(cmdstr).DefaultView
		For Each listitem As ListItem In chkKategoriler.Items
			dv.RowFilter = "KategoriKodu=" + listitem.Value
			If dv.Count > 0 Then listitem.Selected = True
		Next
	End Sub

	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		Saydanclass.LoginKontroluYap()
		If Not Saydanclass.YoneticiMi Then Response.Redirect("index.aspx")
	End Sub

	Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
		Dim cmdstr As String = "SELECT * FROM URUNLER where StokKodu=" + Kod.ToString
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim dr As DataRow
		If Kod = -1 Then
			dr = dt.NewRow
		Else
			dr = dt.Rows(0)
		End If

		dr("StokAdi1") = txtUrunAdi.Text
		dr("KisaAciklama1") = Mid(txtKisaAciklama.Text, 1, 512)
		dr("BarkodNo") = txtBarkodNo.Text
		dr("Miktar") = txtMiktar.Text
		dr("AktifMi") = drpAktifMi.SelectedValue
		dr("AlisFiyati") = txtAlisFiyati.Text
		dr("SatisFiyati") = txtSatisFiyati.Text
		dr("TedarikciFirma") = txtTedarikciFirma.Text
		dr("DetayAciklama1") = txtUrunDetayi.Text
		dr("Keywords") = txtKeyword.Text
		dr("Description") = txtDescription.Text
		If txtResim1.Text <> "" Then dr("Pic1") = txtResim1.Text
		If txtResim2.Text <> "" Then dr("Pic2") = txtResim2.Text
		If txtResim3.Text <> "" Then dr("Pic3") = txtResim3.Text
		If txtResim4.Text <> "" Then dr("Pic4") = txtResim4.Text
		If txtResim5.Text <> "" Then dr("Pic5") = txtResim5.Text
		If Kod = -1 Then dt.Rows.Add(dr)
		Saydanclass._SaveDB(cmdstr, dt)

		cmdstr = "delete from [KATEGORILER_URUNLER] where UrunKodu=" + Kod.ToString + ";"
		Dim siraNo As Integer = 0
		For Each li As ListItem In chkKategoriler.Items
			If li.Selected = True Then
				siraNo += 1
				cmdstr += vbCrLf + "insert into KATEGORILER_URUNLER (KategoriKodu,UrunKodu,SiraNo) Values(" + li.Value.ToString + "," + Kod.ToString + "," + siraNo.ToString + ")"
			End If
		Next
		Saydanclass.ExecSQL(cmdstr)

		lblUyari.Text = "Kayıt işlemi tamamlanmıştır."
		divUyari.Visible = True
	End Sub



	'Protected Sub UrunlerListesi()
	'	Dim hr As HtmlTableRow
	'	Dim hc As HtmlTableCell

	'	Dim cmdstr As String = "SELECT KATEGORILER_URUNLER.KategoriKodu, KATEGORILER_URUNLER.SiraNo, URUNLER.StokKodu, URUNLER.StokAdi1, URUNLER.KisaAciklama1, URUNLER.SatisFiyati, URUNLER.Pic1, URUNLER.AlisFiyati, URUNLER.Miktar, URUNLER.TedarikciFirma FROM KATEGORILER_URUNLER INNER JOIN URUNLER ON KATEGORILER_URUNLER.UrunKodu = URUNLER.StokKodu where KATEGORILER_URUNLER.KategoriKodu=" + Kod.ToString
	'	Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
	'	If dt.Rows.Count = 0 Then
	'		hr = New HtmlTableRow
	'		hc = New HtmlTableCell
	'		hc.ColSpan = 8
	'		hc.InnerHtml = "Bu kategoriye ait ürün bulunmuyor."
	'		hr.Cells.Add(hc)
	'		tblUrunler.Rows.Add(hr)
	'		Exit Sub
	'	End If

	'	Dim SiraNo As Integer = 0
	'	hr = New HtmlTableRow
	'	hc = New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "Seç" : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "Resim" : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "Ürün Adı" : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "Tedarikçi" : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "Miktar" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "<nobr>Alış Fiyatı<nobr><br><nobr>Satış Fiyatı<nobr>" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
	'	hc = New HtmlTableCell : hc.InnerHtml = "İşlem" : hr.Cells.Add(hc)
	'	tblUrunler.Rows.Add(hr)
	'	'lblAdet.Text = dt.Rows.Count.ToString

	'	Dim Urunler As String = ""
	'	Dim SatisFiyati As Decimal = 0
	'	Dim AlisFiyati As Decimal = 0
	'	For SiraNo = 0 To dt.Rows.Count - 1
	'		Dim dr As DataRow = dt.Rows(SiraNo)

	'		hr = New HtmlTableRow
	'		hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = "" : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = "<img src=""" + dr("Pic1").ToString + """ width=""75"" />" : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = dr("StokAdi1").ToString : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = dr("TedarikciFirma").ToString : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = dr("Miktar").ToString : hr.Cells.Add(hc)
	'		hc = New HtmlTableCell : hc.InnerHtml = CDec(dr("AlisFiyati")).ToString("n2") + "<br>" + CDec(dr("SatisFiyati")).ToString("n2") : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)


	'		hc = New HtmlTableCell : hc.InnerHtml = "<a href=""UrunlerDetay.aspx?kod=" + dr("StokKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
	'		tblUrunler.Rows.Add(hr)
	'	Next


	'End Sub

	Protected Sub btnSil_Click(sender As Object, e As EventArgs) Handles btnSil.Click
		If lblUrunKodu.Text = "" Then Return

		Kod = lblUrunKodu.Text

		Dim cmdSQL As String = "Delete from URUNLER where StokKodu=" + Kod.ToString()
		Saydanclass.ExecSQL(cmdSQL)
		Response.Redirect("UrunlerListe.aspx")

	End Sub
End Class
