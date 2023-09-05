Imports System.Data
Partial Class product_details
    Inherits System.Web.UI.Page

	Dim saydanclass As New SaydanClass
	Dim Kod As Integer


	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		Kod = Request.QueryString("kod")
		'If Kod = 0 Then Kod = 25
		FeaturedSection()

		If Session("YoneticiMi") Is Nothing Then Session("YoneticiMi") = False

		If Session("YoneticiMi") = True Then
			divyonetici.Visible = True

			hypUrunDetay.NavigateUrl = "UrunlerDetay.aspx?kod=" + Kod.ToString()
			hypBanneraEkle.NavigateUrl = "BannerDetay.aspx?uk=" + Kod.ToString()

			If Not IsPostBack Then
				If saydanclass._ConnDB("select * from [dbo].[KATEGORILER_URUNLER] where UrunKodu=" + Kod.ToString + " and KategoriKodu=14").Rows.Count = 1 Then chkSecim.Items(0).Selected = True
				If saydanclass._ConnDB("select * from [dbo].[KATEGORILER_URUNLER] where UrunKodu=" + Kod.ToString + " and KategoriKodu=15").Rows.Count = 1 Then chkSecim.Items(1).Selected = True
			End If



		Else
			divyonetici.Visible = False
		End If


	End Sub


	Protected Sub FeaturedSection()

		Dim cmdstr As String = "select  * from URUNLER where StokKodu=" + Kod.ToString
		Dim dt As DataTable = saydanclass._ConnDB(cmdstr)
		If dt.Rows.Count = 0 Then Exit Sub
		Dim dr As DataRow = dt.Rows(0)
		hypAnaResim.ToolTip = dr("StokAdi1")
		hypAnaResim.ImageUrl = dr("Pic1").ToString
		hypAnaResim.NavigateUrl = dr("Pic1").ToString
		hypResim1.ToolTip = dr("StokAdi1")
		hypResim1.ImageUrl = dr("Pic1").ToString
		hypResim1.NavigateUrl = dr("Pic1").ToString
		hypResim2.ToolTip = dr("StokAdi1")
		hypResim2.ImageUrl = dr("Pic2").ToString
		hypResim2.NavigateUrl = dr("Pic2").ToString
		hypResim3.ToolTip = dr("StokAdi1")
		hypResim3.ImageUrl = dr("Pic3").ToString
		hypResim3.NavigateUrl = dr("Pic3").ToString
		lblUrunAdi.InnerText = dr("StokAdi1").ToString
		lblFiyat.InnerText = CDec(dr("SatisFiyati")).ToString("n2") + " TL"
		lblKisaAciklama.InnerHtml = Mid(dr("KisaAciklama1").ToString, 1, 150)
		lblDetay.InnerHtml = dr("DetayAciklama1").ToString
		lblMiktar.InnerText = "Stok Durumu : " + dr("Miktar").ToString + " Adet"
		Page.MetaDescription = dr("Description").ToString
		Page.MetaKeywords = dr("Keywords").ToString
	End Sub

	Protected Sub btnSepet_Click(sender As Object, e As EventArgs) Handles btnSepet.Click
		If Not IsNumeric(txtAdet.Text) Then txtAdet.Text = 1
		Dim whrcnd As String = ""
		If Session("UserID") Is Nothing Then
			whrcnd = "SessionID='" + Session("SessionID").ToString + "'"
		Else
			whrcnd = "MusteriKodu=" + Session("UserID").ToString
		End If
		Dim cmdstr As String = "select top 1 * from SEPET where UrunKodu=" + Kod.ToString + " and " + whrcnd
		Dim dt As DataTable = saydanclass._ConnDB(cmdstr)
		Dim dr As DataRow
		If dt.Rows.Count = 0 Then
			dr = dt.NewRow
		Else
			dr = dt.Rows(0)
		End If
		If Not Session("UserID") Is Nothing Then
			dr("MusteriKodu") = Session("UserID")
		Else
			dr("SessionID") = Session("SessionID").ToString
		End If
		dr("UrunKodu") = Kod
		dr("SepeteEklenmeTarihi") = Now
		If IsDBNull(dr("Miktar")) Then dr("Miktar") = 0
		dr("Miktar") += txtAdet.Text
		If dt.Rows.Count = 0 Then dt.Rows.Add(dr)
		Dim a As String = saydanclass._SaveDB(cmdstr, dt)
		Response.Redirect("./product_details.aspx?" + Page.ClientQueryString)
	End Sub

	Protected Sub btnHemenAl_Click(sender As Object, e As EventArgs) Handles btnHemenAl.Click
		If Not IsNumeric(txtAdet.Text) Then txtAdet.Text = 1
		Dim whrcnd As String = ""
		If Session("UserID") Is Nothing Then
			whrcnd = "SessionID='" + Session("SessionID").ToString + "'"
		Else
			whrcnd = "MusteriKodu=" + Session("UserID").ToString
		End If
		Dim cmdstr As String = "select top 1 * from SEPET where UrunKodu=" + Kod.ToString + " and " + whrcnd
		Dim dt As DataTable = saydanclass._ConnDB(cmdstr)
		Dim dr As DataRow
		If dt.Rows.Count = 0 Then
			dr = dt.NewRow
		Else
			dr = dt.Rows(0)
		End If
		If Not Session("UserID") Is Nothing Then
			dr("MusteriKodu") = Session("UserID")
		Else
			dr("SessionID") = Session("SessionID").ToString
		End If
		dr("UrunKodu") = Kod
		dr("SepeteEklenmeTarihi") = Now
		If IsDBNull(dr("Miktar")) Then dr("Miktar") = 0
		dr("Miktar") += txtAdet.Text
		dt.Rows.Add(dr)
		saydanclass._SaveDB(cmdstr, dt)
		Response.Redirect("./product_summary.aspx")
	End Sub

	Protected Sub btnKaydet_Click(sender As Object, e As EventArgs) Handles btnKaydet.Click
		If chkSecim.Items(0).Selected = True Then
			Dim cmdstr As String = "select top 1 * from [dbo].[KATEGORILER_URUNLER] where UrunKodu=" + Kod.ToString + " and KategoriKodu=14"
			Dim dr As DataRow
			Dim dt As DataTable = saydanclass._ConnDB(cmdstr)
			If dt.Rows.Count = 0 Then
				dr = dt.NewRow
			Else
				dr = dt.Rows(0)
			End If
			dr("KategoriKodu") = 14
			dr("UrunKodu") = Kod
			If dt.Rows.Count = 0 Then dt.Rows.Add(dr)
			saydanclass._SaveDB(cmdstr, dt)
		Else
			saydanclass.ExecSQL("delete from KATEGORILER_URUNLER where UrunKodu=" + Kod.ToString + " and KategoriKodu=14")
		End If
		If chkSecim.Items(1).Selected = True Then
			Dim cmdstr As String = "select top 1 * from [dbo].[KATEGORILER_URUNLER] where UrunKodu=" + Kod.ToString + " and KategoriKodu=15"
			Dim dr As DataRow
			Dim dt As DataTable = saydanclass._ConnDB(cmdstr)
			If dt.Rows.Count = 0 Then
				dr = dt.NewRow
			Else
				dr = dt.Rows(0)
			End If
			dr("KategoriKodu") = 15
			dr("UrunKodu") = Kod
			If dt.Rows.Count = 0 Then dt.Rows.Add(dr)
			saydanclass._SaveDB(cmdstr, dt)
		Else
			saydanclass.ExecSQL("delete from KATEGORILER_URUNLER where UrunKodu=" + Kod.ToString + " and KategoriKodu=15")
		End If
	End Sub
End Class
