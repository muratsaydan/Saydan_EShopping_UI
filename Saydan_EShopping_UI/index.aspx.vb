Imports System.Data


Partial Class index
    Inherits System.Web.UI.Page

	Dim saydanclass As New SaydanClass

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		Page.MetaDescription = "Mersin Borsası, Mersin ve Çukurova Bölgesine özgü değerlerin internet kullanıcılarıyla buluşturulduğu çok amaçlı bir e-ticaret sitesidir. Burada Mersin'e hizmet veren markaları ve ürünlerini bulabilir, tek tıkla sipariş verebilirsiniz."
		Page.MetaKeywords = "mersin borsası, mersin, borsa, cukurova, çukurova, otantik, yerel, bölgesel, lezzet, e-ticaret,e-store, alis-veris,alış-veriş,mersina, tarsus, silifke"



		'Dim whrcnd As String
		'If Not Session("UserID") = Nothing Then
		'	whrcnd = "SEPET.MusteriKodu = " + Session("UserID").ToString
		'Else
		'	whrcnd = "SEPET.SessionID = '" + Session("SessionID").ToString.ToString + "'"
		'End If
		'Dim dtSepet As System.Data.DataTable = saydanclass._ConnDB("select count(*) adet from (SELECT distinct [UrunKodu] FROM [SEPET] where " + whrcnd + " group by UrunKodu ) as A")
		'Dim SepettekiUrunKalemAdedi As Integer = dtSepet.Rows(0)(0)
		'dtSepet = saydanclass._ConnDB("SELECT SUM(URUNLER.SatisFiyati * SEPET.Miktar) AS Tutar FROM SEPET INNER JOIN URUNLER ON SEPET.UrunKodu = URUNLER.StokKodu where " + whrcnd)
		'Dim SepettekiUrunTutari As Integer = 0
		'If dtSepet.Rows.Count > 0 Then
		'	If IsDBNull(dtSepet.Rows(0)(0)) Then dtSepet.Rows(0)(0) = 0
		'	SepettekiUrunTutari = dtSepet.Rows(0)(0)
		'End If

		'lblSepettekiUrunTutari2.Text = SepettekiUrunTutari.ToString("n2") + " TL"

		'lblUrunKalemAdedi2.Text = SepettekiUrunKalemAdedi





		'SideBarOlustur()
		'FeaturedSection()
		'SpecialsSection()
		'BannerlariOlustur()
	End Sub

	Protected Sub BannerlariOlustur()
		Dim dtBanner = saydanclass._ConnDB("select * from BANNER")
		DivBanner.InnerHtml = ""
		Dim İlkMi As Boolean = True
		For Each drBanner As DataRow In dtBanner.Rows
			Dim tmp As String = ""
			If İlkMi = True Then
				tmp = "active"
				İlkMi = False
			End If
			DivBanner.InnerHtml += vbCrLf + "<div class=""item " + tmp + """><div class=""container""><a href=""" + drBanner("Link").ToString + """ target=""_blank""><img height=""700"" src=""" + drBanner("ResimURL").ToString + """ alt=""" + drBanner("Link").ToString + """/></a><div class=""carousel-caption""><h4>" + drBanner("UrunAdi").ToString + "</h4>" + drBanner("BannerAciklamasi").ToString + "</div></div></div>"
		Next
	End Sub

	Protected Sub SideBarOlustur()
		Dim dtRaw As DataTable = saydanclass._ConnDB("SELECT        KATEGORILER.KategoriKodu, KATEGORILER.StokGrupAdi1, KATEGORILER.EbeveynKategoriKodu, COUNT(KATEGORILER_URUNLER.KUrunKodu) AS UrunAdedi, KATEGORILER.Siralama FROM            KATEGORILER LEFT OUTER JOIN KATEGORILER_URUNLER ON KATEGORILER.KategoriKodu = KATEGORILER_URUNLER.KategoriKodu GROUP BY KATEGORILER.KategoriKodu, KATEGORILER.EbeveynKategoriKodu, KATEGORILER.StokGrupAdi1, KATEGORILER.Siralama ORDER BY KATEGORILER.Siralama, KATEGORILER.KategoriKodu")
		'Dim dv As DataView = dt.DefaultView

		Dim dt As DataTable = SaydanClass.Hiyerarsik_Tablo_Olusturma(dtRaw, "KategoriKodu", "EbeveynKategoriKodu")
		Dim cmdHTML As String = "<ul  id=""sideManu"" class=""nav nav-tabs nav-stacked"">"
		For siraNo = 0 To dt.Rows.Count - 1
			Dim drSiradakiKayit As DataRow = dt.Rows(siraNo)

			Dim Kod As Integer = drSiradakiKayit("KategoriKodu")
			Dim Level As Integer = drSiradakiKayit("__Level")

			If siraNo = dt.Rows.Count - 1 Then
				'eğer son kayda ulaştıysan (ardindaki kayit olmayacak
				'seviye 0 ise <li>xx</li>
				'seviye 1 ise <li>xx</li></ul></li>
				'Seviye 2 ise <li>xx</li> </ul></li> </ul></li>
				'Seviye 3 ise <li>xx</li> </ul></li> </ul></li> </ul></li>

				cmdHTML += vbCrLf + "<li> <a href=""products.aspx?kod=" + dt.Rows(siraNo)("KategoriKodu").ToString + """>" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</a></li>"
				For Seviye = Level - 1 To 1 Step -1
					cmdHTML += vbCrLf + "</ul></li> "
				Next
			Else
				If dt.Rows(siraNo)("__Level") < dt.Rows(siraNo + 1)("__Level") Then
					'eğer ardındaki kaydin seviyesi daha yüksekse <li>xx<ul> ekle
					cmdHTML += vbCrLf + "<li class=""subMenu""><a href=""#"">" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</a><ul>"
				ElseIf dt.Rows(siraNo)("__Level") = dt.Rows(siraNo + 1)("__Level") Then
					'eğer ardındaki kaydin seviyesi aynı ise "<li>xx</li> şeklide bitir
					cmdHTML += vbCrLf + "<li><a href=""products.aspx?kod=" + dt.Rows(siraNo)("KategoriKodu").ToString + """>" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</a></li>"
				ElseIf dt.Rows(siraNo)("__Level") > dt.Rows(siraNo + 1)("__Level") Then
					'eğer ardındaki kaydin seviyesi düşükse<li>xx</li></ul></li> ile bitir
					cmdHTML += vbCrLf + "<li><a href=""products.aspx?kod=" + dt.Rows(siraNo)("KategoriKodu").ToString + """>" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</a></li>"
					Dim BirSonrakiSeviye As Integer = dt.Rows(siraNo + 1)("__Level")
					For Seviye = Level - 1 To BirSonrakiSeviye Step -1
						cmdHTML += vbCrLf + "</ul></li>"
					Next
				End If
			End If

		Next
		cmdHTML += vbCrLf + "</ul>"
		spnSideBar.InnerHtml = cmdHTML

	End Sub

	Protected Sub FeaturedSection()

		Dim GosterilecekKategoriAdi As String = "İndirimdeki Ürünler"


		Dim FeatProdCount As Integer = saydanclass._ConnDB("SELECT count (*) FROM  KATEGORILER_URUNLER INNER JOIN URUNLER ON KATEGORILER_URUNLER.UrunKodu = URUNLER.StokKodu INNER JOIN KATEGORILER ON KATEGORILER_URUNLER.KategoriKodu = KATEGORILER.KategoriKodu WHERE   URUNLER.AktifMi=1  and URUNLER.Miktar>0 and       (KATEGORILER.StokGrupAdi1 = '" + GosterilecekKategoriAdi + "') ").Rows(0)(0)


		Dim kalan As Integer = FeatProdCount Mod 4

		FeatProdCount -= kalan

		If FeatProdCount > 20 Then FeatProdCount = 20

		lblFeatCnt.Text = FeatProdCount



		Dim cmdstr As String = "SELECT Top 20 URUNLER.StokKodu, URUNLER.StokAdi1, URUNLER.SatisFiyati, URUNLER.Pic1 FROM KATEGORILER_URUNLER INNER JOIN URUNLER ON KATEGORILER_URUNLER.UrunKodu = URUNLER.StokKodu INNER JOIN KATEGORILER ON KATEGORILER_URUNLER.KategoriKodu = KATEGORILER.KategoriKodu WHERE  URUNLER.AktifMi=1 and URUNLER.Miktar>0 and  (KATEGORILER.StokGrupAdi1 = '" + GosterilecekKategoriAdi + "') Order by KATEGORILER_URUNLER.SiraNo"
		Dim dt As DataTable = saydanclass._ConnDB(cmdstr)
		Dim cmdhtml As String = ""
		cmdhtml += vbCrLf + "			<div id=""featured"" class=""carousel slide"">"
		cmdhtml += vbCrLf + "			<div class=""carousel-inner"">"

		For SiraNo = 0 To FeatProdCount - 1
			Dim IlkKayit As String = ""
			Select Case SiraNo
				Case 0
					cmdhtml += vbCrLf + "			  <div class=""item active"">"
					cmdhtml += vbCrLf + "			  <ul class=""thumbnails"">"
				Case 4, 8, 12, 16
					cmdhtml += vbCrLf + "			  </ul>"
					cmdhtml += vbCrLf + "			  </div>"
					cmdhtml += vbCrLf + ""
					cmdhtml += vbCrLf + "			  <div class=""item"">"
					cmdhtml += vbCrLf + "			  <ul class=""thumbnails"">"
			End Select
			Dim dr As DataRow = dt.Rows(SiraNo)
			If IsDBNull(dr("SatisFiyati")) Then dr("SatisFiyati") = 0

			cmdhtml += vbCrLf + "				<li class=""span3"">"
			cmdhtml += vbCrLf + "				  <div class=""thumbnail"">"
			cmdhtml += vbCrLf + "				  <i class=""tag""></i><div style=""height:250px"">"
			cmdhtml += vbCrLf + "					<a href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """><img src=""" + dr("Pic1").ToString + """ alt=""""></a></div>"
			cmdhtml += vbCrLf + "					<div class=""caption"" ><div style=""height:100px;"">"
			cmdhtml += vbCrLf + "					  <h5>" + dr("StokAdi1").ToString + "</h5></div>"
			cmdhtml += vbCrLf + "					  <h4><a class=""btn"" href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """>Detay</a> <span class=""pull-right"">" + CDec(dr("SatisFiyati")).ToString("n2") + " TL</span></h4>"
			cmdhtml += vbCrLf + "					</div>"
			cmdhtml += vbCrLf + "				  </div>"
			cmdhtml += vbCrLf + "				</li>"
		Next
		cmdhtml += vbCrLf + "			  </ul>"
		cmdhtml += vbCrLf + "			  </div>"
		cmdhtml += vbCrLf + ""
		cmdhtml += vbCrLf + "			  </div>"
		cmdhtml += vbCrLf + "			  <a class=""left carousel-control"" href=""#featured"" data-slide=""prev"">‹</a>"
		cmdhtml += vbCrLf + "			  <a class=""right carousel-control"" href=""#featured"" data-slide=""next"">›</a>"
		cmdhtml += vbCrLf + "			  </div>"

		spnFeat.InnerHtml = cmdhtml
	End Sub

	Protected Sub SpecialsSection()
		'Dim FeatProdCount As Integer = saydanclass._ConnDB("SELECT count (*) FROM  KATEGORILER_URUNLER INNER JOIN URUNLER ON KATEGORILER_URUNLER.UrunKodu = URUNLER.StokKodu INNER JOIN KATEGORILER ON KATEGORILER_URUNLER.KategoriKodu = KATEGORILER.KategoriKodu WHERE        (KATEGORILER.StokGrupAdi1 = 'Fiyatı Düşenler') ").Rows(0)(0)

		'lblFeatCnt.Text = FeatProdCount

		Dim cmdstr As String = "SELECT Top 20 URUNLER.StokKodu, URUNLER.StokAdi1, URUNLER.SatisFiyati, URUNLER.Pic1, URUNLER.KisaAciklama1 FROM KATEGORILER_URUNLER INNER JOIN URUNLER ON KATEGORILER_URUNLER.UrunKodu = URUNLER.StokKodu INNER JOIN KATEGORILER ON KATEGORILER_URUNLER.KategoriKodu = KATEGORILER.KategoriKodu WHERE  URUNLER.AktifMi=1 and URUNLER.Miktar>0  and (KATEGORILER.StokGrupAdi1 = 'Özel Seçtiklerimiz') Order by KATEGORILER_URUNLER.SiraNo"
		Dim dt As DataTable = saydanclass._ConnDB(cmdstr)
		Dim cmdhtml As String = ""
		cmdhtml += vbCrLf + "						  <ul class=""thumbnails"">"
		For SiraNo = 0 To dt.Rows.Count - 1
			Dim dr As DataRow = dt.Rows(SiraNo)
			If IsDBNull(dr("SatisFiyati")) Then dr("SatisFiyati") = 0

			cmdhtml += vbCrLf + "					<li class=""span3"">"
			cmdhtml += vbCrLf + "					  <div class=""thumbnail"">"
			cmdhtml += vbCrLf + "						<a  href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """><div style=""height:260px"">"
			cmdhtml += vbCrLf + "<img src=""" + dr("Pic1").ToString + """ alt=""" + dr("StokAdi1").ToString + """/></div></a>"
			cmdhtml += vbCrLf + "						<div class=""caption""><div style=""height:50px;"">"
			cmdhtml += vbCrLf + "						  <h5>" + dr("StokAdi1").ToString + "</h5></div>"
			cmdhtml += vbCrLf + "						<h4 style=""text-align: center""><a class=""btn"" href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """>Detay<i class=""icon-shopping-cart""></i></a><a class=""btn btn-primary"" href=""product_details.aspx?kod=" + dr("StokKodu").ToString + """>" + CDec(dr("SatisFiyati")).ToString("n2") + " TL</a></h4>"
			cmdhtml += vbCrLf + "						</div>"
			cmdhtml += vbCrLf + "					  </div>"
			cmdhtml += vbCrLf + "					</li>"
		Next
		cmdhtml += vbCrLf + "			  </ul>"
		spnSpec.InnerHtml = cmdhtml
	End Sub
End Class
