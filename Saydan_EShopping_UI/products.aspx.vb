Imports System.Data

Partial Class products
	Inherits System.Web.UI.Page

	Dim SaydanClass As New SaydanClass

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim Kod As Integer = Request.QueryString("kod")
		Dim KategoriAdi As String = "Tüm Ürünler"
		Dim drKategori As DataRow
		If Kod > 0 Then
			drKategori = SaydanClass._ConnDB("select [StokGrupAdi1], Detay,Keywords,Description from KATEGORILER where KategoriKodu=" + Kod.ToString).Rows(0)
			KategoriAdi = drKategori(0).ToString
			spnUrunKategorisiAciklama.InnerHtml = drKategori(1).ToString
			If (drKategori("Keywords").ToString <> "") Then
				Page.MetaKeywords = drKategori("Keywords").ToString
			Else
				Page.MetaKeywords = drKategori("StokGrupAdi1").ToString
			End If
			If (drKategori("Description").ToString <> "") Then
				Page.MetaDescription = drKategori("Description").ToString
			Else
				Page.MetaDescription = drKategori("StokGrupAdi1").ToString
			End If
			Page.Title = drKategori("StokGrupAdi1").ToString

		Else
			spnUrunKategorisiAciklama.InnerHtml = ""
		End If

		lblUrunKategorisi.Text = KategoriAdi

		Dim dtKategoriler As DataTable = SaydanClass._ConnDB("SELECT KategoriKodu, isnull(EbeveynKategoriKodu,0) EbeveynKategoriKodu FROM KATEGORILER")
		Dim dthiy As DataTable = SaydanClass.AltKayitlariniBul(dtKategoriler, "KategoriKodu", "EbeveynKategoriKodu", Kod, True)
		Dim whrcnd As String = ""
		For Each dr As DataRow In dthiy.Rows
			whrcnd += "," + dr(0).ToString
		Next

		Dim cmdstr As String = "SELECT DISTINCT URUNLER.* FROM KATEGORILER_URUNLER INNER JOIN URUNLER ON KATEGORILER_URUNLER.UrunKodu = URUNLER.StokKodu where  URUNLER.AktifMi=1 and URUNLER.Miktar>0  and KATEGORILER_URUNLER.KategoriKodu in (" + Mid(whrcnd, 2) + ")"
		Dim dt As DataTable = SaydanClass._ConnDB(cmdstr)
		lblUrunAdedi.Text = dt.Rows.Count.ToString
		Dim cmdHtml1 As String = "<ul class=""thumbnails"">"
		Dim cmdHTML2 As String = ""
		For Each dr As DataRow In dt.Rows
			If IsDBNull(dr("SatisFiyati")) Then dr("SatisFiyati") = 0
			cmdHtml1 += vbCrLf + "			<li class=""span3"">"
			cmdHtml1 += vbCrLf + "				<div class=""thumbnail"" >"
			cmdHtml1 += vbCrLf + "					<a href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """><div style=""height:260px"">"
			cmdHtml1 += vbCrLf + "					<img src=""" + dr("pic1").ToString + """ alt=""" + dr("StokAdi1").ToString + """ /></div></a>"
			cmdHtml1 += vbCrLf + "					<div class=""caption"" style=""height:50px"">"
			cmdHtml1 += vbCrLf + "						<h5>" + dr("StokAdi1").ToString + "</h5>"
			'cmdHtml1 += vbCrLf + "						<p>"
			'cmdHtml1 += vbCrLf + "							" + Mid(dr("KisaAciklama1").ToString, 1, 50) + "..."
			'cmdHtml1 += vbCrLf + "						</p>"
			cmdHtml1 += vbCrLf + "					</div>"
			cmdHtml1 += vbCrLf + "						<h4 style=""text-align: center""><a class=""btn"" href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """><i class=""icon-zoom-in""></i></a><a class=""btn"" href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """>Detay<i class=""icon-shopping-cart""></i></a><a class=""btn btn-primary"" href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """>" + CDec(dr("SatisFiyati")).ToString("n2") + " TL</a></h4>"
			cmdHtml1 += vbCrLf + "				</div>"
			cmdHtml1 += vbCrLf + "			</li>"


			cmdHTML2 += vbCrLf + "<div class=""row"">"
			cmdHTML2 += vbCrLf + "	<div class=""span2"">"
			cmdHTML2 += vbCrLf + "		<img src=""" + dr("pic1").ToString + """ alt=""" + dr("StokAdi1").ToString + """ />"
			cmdHTML2 += vbCrLf + "	</div>"
			cmdHTML2 += vbCrLf + "	<div class=""span4"" style=""height:50px"">"
			'cmdHTML2 += vbCrLf + "		<h3>New | Available</h3>"
			'cmdHTML2 += vbCrLf + "		<hr class=""soft"" />"
			cmdHTML2 += vbCrLf + "		<h5>" + dr("StokAdi1").ToString + "</h5>"
			'cmdHTML2 += vbCrLf + "		<p>"
			'cmdHTML2 += vbCrLf + "			" + dr("KisaAciklama1").ToString
			'cmdHTML2 += vbCrLf + "		</p>"
			'cmdHTML2 += vbCrLf + "		<a class=""btn btn-small pull-right"" href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """>Detay</a>"
			cmdHTML2 += vbCrLf + "		<br class=""clr"" />"
			cmdHTML2 += vbCrLf + "	</div>"
			cmdHTML2 += vbCrLf + "	<div class=""span3 alignR"">"
			cmdHTML2 += vbCrLf + "		<form class=""form-horizontal qtyFrm"">"
			cmdHTML2 += vbCrLf + "			<h3>" + CDec(dr("SatisFiyati")).ToString("n2") + " TL</h3>"
			'cmdHTML2 += vbCrLf + "			<label class=""checkbox"">"
			'cmdHTML2 += vbCrLf + "				<input type=""checkbox"">"
			'cmdHTML2 += vbCrLf + "				Adds product to compair"
			'cmdHTML2 += vbCrLf + "			</label>"
			cmdHTML2 += vbCrLf + "			<br />"
			cmdHTML2 += vbCrLf + ""
			cmdHTML2 += vbCrLf + "			<a href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """ class=""btn btn-large btn-primary"">Sepete Ekle <i class="" icon-shopping-cart""></i></a>"
			cmdHTML2 += vbCrLf + "			<a href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """ class=""btn btn-large""><i class=""icon-zoom-in""></i></a>"
			cmdHTML2 += vbCrLf + ""
			cmdHTML2 += vbCrLf + "		</form>"
			cmdHTML2 += vbCrLf + "	</div>"
			cmdHTML2 += vbCrLf + "</div>" + vbCrLf + "<hr class=""soft"" />"
			Page.MetaKeywords += "," + dr("StokAdi1").ToString
		Next
		cmdHtml1 += vbCrLf + "</ul>"
		spnListe2.InnerHtml = cmdHtml1
		spnListe1.InnerHtml = cmdHTML2

	End Sub
End Class
