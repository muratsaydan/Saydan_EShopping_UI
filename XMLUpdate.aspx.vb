Imports System.Xml
Imports System.Data

Partial Class SiparisListe
	Inherits System.Web.UI.Page

	Dim okuyucu As XmlTextReader
	Dim dokuman As XmlDocument
	Dim SaydanClass As New SaydanClass


	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		'Saydanclass.LoginKontroluYap()
		'If Not Saydanclass.YoneticiMi Then Response.Redirect("SiparisListe.aspx")
	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		Dim cmdstr As String = "select * from XML_ENTEGRASYON where AktifMi=1 order by Siralama"
		Dim dt As DataTable = SaydanClass._ConnDB(cmdstr)
		For Each dr As DataRow In dt.Rows
			Dim btn As New LinkButton
			btn.ID = "btn_" + dr("Kod").ToString
			AddHandler btn.Click, AddressOf btn_click
			btn.Attributes.Add("XMLAdresi", dr("XMLAdresi").ToString)
			btn.Attributes.Add("TedarikciFirma", dr("TedarikciFirma").ToString)
			btn.Attributes.Add("EntegrasyonTipi", dr("EntegrasyonTipi").ToString)
			btn.Text = dr("TedarikciFirma").ToString + "<br /><br />"
			spnbutonlar.Controls.Add(btn)
			btn = New LinkButton
			btn.ID = "btn2_" + dr("Kod").ToString
			AddHandler btn.Click, AddressOf btnN11_click
			btn.Attributes.Add("XMLAdresi", dr("XMLAdresi").ToString)
			btn.Attributes.Add("TedarikciFirma", dr("TedarikciFirma").ToString)
			btn.Attributes.Add("EntegrasyonTipi", dr("EntegrasyonTipi").ToString)
			btn.Text = dr("TedarikciFirma").ToString + "<br /><br />"
			spnN11eHazirla.Controls.Add(btn)
		Next
	End Sub

	Protected Sub btn_click(sender As LinkButton, e As EventArgs)
		Dim TedarikciFirma As String = sender.Attributes("TedarikciFirma").ToString
		Dim XMLAdresi As String = sender.Attributes("XMLAdresi").ToString
		Dim EntegrasyonTipi As String = sender.Attributes("EntegrasyonTipi").ToString
		Select Case EntegrasyonTipi
			Case "N11"
				N11_comdanYukle(XMLAdresi, TedarikciFirma)
			Case "IcGiyim"
				IcGiyimRuzgarindan_Yukle(XMLAdresi, TedarikciFirma)
			Case "Bayi Kanalı"
				BayiKanalindan_Yukle(XMLAdresi, TedarikciFirma)
		End Select
	End Sub

	Protected Sub btnN11_click(sender As LinkButton, e As EventArgs)
		Dim TedarikciFirma As String = sender.Attributes("TedarikciFirma").ToString
		Dim XMLAdresi As String = sender.Attributes("XMLAdresi").ToString
		Dim EntegrasyonTipi As String = sender.Attributes("EntegrasyonTipi").ToString
		Select Case EntegrasyonTipi
			Case "N11"
				N11_com_XMLinde_FiyatGuncelle(XMLAdresi, TedarikciFirma)
		End Select
	End Sub
	Protected Sub BayiKanalindan_Yukle(ByVal XMLAdresi As String, ByVal TedarikciFirma As String)
		okuyucu = New XmlTextReader(XMLAdresi)
		dokuman = New XmlDocument
		Try
			dokuman.Load(okuyucu)
		Catch ex As Exception
			Exit Sub
		End Try
		Dim GuncellemeTarihi As String = Now.ToString("dd.MM.yyyy HH:mm:ss")
		'Dim cmdhtml As String = "<table style=""padding:10px; border-collapse:collapse;"" border=""1""><thead><tr><td>#</td><td>BarkodNo</td><td>Ürün Adı</td> <td> Kategori1</td><td> Alış fiyatı</td><td> İndirimsiz Fiyat</td><td> KDV Oranı</td><td>Miktar </td><td>Resim1</td><td>Resim2</td><td>Resim3</td></tr></thead><tbody>"

		For i = 0 To dokuman.Item("ROOT").ChildNodes(0).ChildNodes.Count - 1
			Dim StokAdi As String = dokuman.Item("ROOT").ChildNodes(0).ChildNodes(i).Attributes("Stok_Ad").Value
			Dim Kategori As String = dokuman.Item("ROOT").ChildNodes(0).ChildNodes(i).Attributes("AltUrunGrup_Ad").Value
			Dim BarkodKodu As String = dokuman.Item("ROOT").ChildNodes(0).ChildNodes(i).Attributes("Stok_Kod").Value
			Dim Aciklama As String = dokuman.Item("ROOT").ChildNodes(0).ChildNodes(i).Attributes("Stok_Ad").Value + " " + dokuman.Item("ROOT").ChildNodes(0).ChildNodes(i).Attributes("Stok_Ad2").Value
			Dim AlisFiyati As String = dokuman.Item("ROOT").ChildNodes(0).ChildNodes(i).Attributes("BayiOzel_Fiyat").Value.Replace(".", ",")
			Dim IndirimsizAlisFiyati As String = dokuman.Item("ROOT").ChildNodes(0).ChildNodes(i).Attributes("BayiOzel_Fiyat").Value.Replace(".", ",")
			Dim SatisFiyati As String = dokuman.Item("ROOT").ChildNodes(0).ChildNodes(i).Attributes("SonKullanici_Fiyat").Value.Replace(".", ",")

			Dim Miktar As String = dokuman.Item("ROOT").ChildNodes(0).ChildNodes(i).Attributes("Miktar").Value
			Dim StokKodu As Integer = 0
			'Ürünün varolup olmadığını kontrol et (Barkod Nosunu Baz Al)
			Dim cmdstr As String = "select * from URUNLER where TedarikciFirma='" + TedarikciFirma + "' and BarkodNo='" + BarkodKodu + "'"
			Dim dtUrun As DataTable = SaydanClass._ConnDB(cmdstr)
			Dim drUrun As DataRow
			Dim DegisiklikVarmi As Boolean = False
			If dtUrun.Rows.Count > 0 Then
				drUrun = dtUrun.Rows(0)
				StokKodu = drUrun("StokKodu").ToString
			Else
				drUrun = dtUrun.NewRow
				DegisiklikVarmi = True
			End If
			If drUrun("StokAdi1").ToString <> StokAdi Then drUrun("StokAdi1") = StokAdi : DegisiklikVarmi = True
			If drUrun("DetayAciklama1").ToString <> Aciklama Then drUrun("DetayAciklama1") = Aciklama : DegisiklikVarmi = True
			If drUrun("BarkodNo").ToString <> BarkodKodu Then drUrun("BarkodNo") = BarkodKodu : DegisiklikVarmi = True
			drUrun("KDVOrani") = dokuman.Item("ROOT").ChildNodes(0).ChildNodes(i).Attributes("Kdv").Value
			drUrun("TedarikciFirma") = TedarikciFirma
			If IsDBNull(drUrun("IndirimsizAlisFiyati")) Then drUrun("IndirimsizAlisFiyati") = 0
			If IsDBNull(drUrun("AlisFiyati")) Then drUrun("AlisFiyati") = 0
			If IsDBNull(drUrun("SatisFiyati")) Then drUrun("SatisFiyati") = 0
			If drUrun("IndirimsizAlisFiyati") <> IndirimsizAlisFiyati Then drUrun("IndirimsizAlisFiyati") = IndirimsizAlisFiyati : DegisiklikVarmi = True
			If drUrun("AlisFiyati") <> AlisFiyati Then drUrun("AlisFiyati") = AlisFiyati : DegisiklikVarmi = True
			drUrun("AlisFiyatiPB") = 1
			If drUrun("Miktar").ToString <> Miktar Then drUrun("Miktar") = Miktar : DegisiklikVarmi = True

			'Dim SatisFiyati As Decimal = AlisFiyati * 1.2


			If drUrun("KisaAciklama1").ToString <> StokAdi Then drUrun("KisaAciklama1") = StokAdi : DegisiklikVarmi = True
			If drUrun("SatisFiyati").ToString <> SatisFiyati Then drUrun("SatisFiyati") = SatisFiyati : DegisiklikVarmi = True
			drUrun("SatisFiyatiPB") = 1
			If drUrun("Keywords").ToString <> StokAdi Then drUrun("Keywords") = StokAdi + "," + Kategori : DegisiklikVarmi = True
			If drUrun("Description").ToString <> StokAdi Then drUrun("Description") = StokAdi : DegisiklikVarmi = True
			drUrun("GuncellemeTarihi") = GuncellemeTarihi
			drUrun("AktifMi") = True

			If dtUrun.Rows.Count = 0 Then dtUrun.Rows.Add(drUrun)
			If DegisiklikVarmi Then SaydanClass._SaveDB(cmdstr, dtUrun)

			If StokKodu = 0 Then
				StokKodu = SaydanClass._ConnDB(cmdstr).Rows(0)("StokKodu").ToString
			End If
			'4 kategorinin de sistemde tanımlı olup olmadığını kontrol et, değilse tanımla
			Dim KategoriKodu As Integer = 0
			cmdstr = "select * from [dbo].[KATEGORILER] where [XMLDegeri]='" + Kategori + "'"
			Dim dtKategori As DataTable = SaydanClass._ConnDB(cmdstr)
			If dtKategori.Rows.Count = 0 Then
				Dim drKategori = dtKategori.NewRow
				drKategori("StokGrupAdi1") = Kategori
				drKategori("XMLDegeri") = Kategori
				drKategori("Siralama") = 0
				dtKategori.Rows.Add(drKategori)
				SaydanClass._SaveDB(cmdstr, dtKategori)
				KategoriKodu = SaydanClass._ConnDB(cmdstr).Rows(0)("KategoriKodu").ToString
			Else
				KategoriKodu = dtKategori.Rows(0)("KategoriKodu")
			End If

			cmdstr = "select * from [dbo].[KATEGORILER_URUNLER] where KategoriKodu=" + KategoriKodu.ToString + " and UrunKodu=" + StokKodu.ToString
			Dim dtKategoriUrun As DataTable = SaydanClass._ConnDB(cmdstr)
			If dtKategoriUrun.Rows.Count = 0 Then
				Dim drKategoriUrun As DataRow = dtKategoriUrun.NewRow
				drKategoriUrun("KategoriKodu") = KategoriKodu
				drKategoriUrun("UrunKodu") = StokKodu
				dtKategoriUrun.Rows.Add(drKategoriUrun)
				SaydanClass._SaveDB(cmdstr, dtKategoriUrun)
			End If

			'cmdhtml += vbCrLf + "<tr><td>" + (i + 1).ToString + "</td><td>" + BarkodKodu + "</td><td>" + StokAdi + "</td> <td> " + Kategori + "</td<td> " + AlisFiyati + "</td><td> " + IndirimsizAlisFiyati + "</td><td>18</td><td>" + Miktar + "</td><td><img src=""" + Resim1 + """ width=""50"" alt=""" + StokAdi + """ /></td><td><img src=""" + Resim2 + """ width=""50"" alt=""" + StokAdi + """ /></td><td><img src=""" + Resim3 + """ width=""50"" alt=""" + StokAdi + """ /></td></tr>"


		Next
		Dim cmdstr2 As String = "UPDATE URUNLER SET AktifMi=0 where TedarikciFirma='" + TedarikciFirma + "' and GuncellemeTarihi <> '" + GuncellemeTarihi + "'"
		SaydanClass.ExecSQL(cmdstr2)
		'cmdhtml += vbCrLf + " </tbody></table>"
		'spn1.InnerHtml = cmdhtml
	End Sub


	Protected Sub IcGiyimRuzgarindan_Yukle(ByVal XMLAdresi As String, ByVal TedarikciFirma As String)
		okuyucu = New XmlTextReader(XMLAdresi)
		dokuman = New XmlDocument
		Try
			dokuman.Load(okuyucu)
		Catch ex As Exception
			Exit Sub
		End Try
		Dim GuncellemeTarihi As String = Now.ToString("dd.MM.yyyy HH:mm:ss")
		Dim cmdhtml As String = "<table style=""padding:10px; border-collapse:collapse;"" border=""1""><thead><tr><td>#</td><td>BarkodNo</td><td>Ürün Adı</td> <td> Kategori1</td><td> Alış fiyatı</td><td> İndirimsiz Fiyat</td><td> KDV Oranı</td><td>Miktar </td><td>Resim1</td><td>Resim2</td><td>Resim3</td></tr></thead><tbody>"

		For i = 0 To dokuman.Item("urunler").ChildNodes.Count - 1
			Dim StokAdi As String = dokuman.Item("urunler").ChildNodes(i).SelectNodes("baslik").Item(0).InnerText
			Dim Resim1 As String = ""
			Dim Resim2 As String = ""
			Dim Resim3 As String = ""
			Dim Resim4 As String = ""
			Dim Resim5 As String = ""
			If Not dokuman.Item("urunler").ChildNodes(i).SelectNodes("resimler").Item(0).ChildNodes(0) Is Nothing Then Resim1 = dokuman.Item("urunler").ChildNodes(i).SelectNodes("resimler").Item(0).ChildNodes(0).InnerText
			If Not dokuman.Item("urunler").ChildNodes(i).SelectNodes("resimler").Item(0).ChildNodes(1) Is Nothing Then Resim2 = dokuman.Item("urunler").ChildNodes(i).SelectNodes("resimler").Item(0).ChildNodes(1).InnerText
			If Not dokuman.Item("urunler").ChildNodes(i).SelectNodes("resimler").Item(0).ChildNodes(2) Is Nothing Then Resim3 = dokuman.Item("urunler").ChildNodes(i).SelectNodes("resimler").Item(0).ChildNodes(2).InnerText
			If Not dokuman.Item("urunler").ChildNodes(i).SelectNodes("resimler").Item(0).ChildNodes(3) Is Nothing Then Resim4 = dokuman.Item("urunler").ChildNodes(i).SelectNodes("resimler").Item(0).ChildNodes(3).InnerText
			If Not dokuman.Item("urunler").ChildNodes(i).SelectNodes("resimler").Item(0).ChildNodes(4) Is Nothing Then Resim5 = dokuman.Item("urunler").ChildNodes(i).SelectNodes("resimler").Item(0).ChildNodes(4).InnerText
			Dim kategori As String = ""
			'Dim kategori2 As String = ""
			'Dim kategori3 As String = ""
			'Dim kategori4 As String = ""
			kategori = dokuman.Item("urunler").ChildNodes(i).SelectNodes("kategori").Item(0).ChildNodes(0).Value
			Dim Barkodkodu As String = dokuman.Item("urunler").ChildNodes(i).SelectNodes("kod").Item(0).InnerText
			Dim aciklama As String = dokuman.Item("urunler").ChildNodes(i).SelectNodes("aciklama").Item(0).InnerText
			Dim Alisfiyati As String = dokuman.Item("urunler").ChildNodes(i).SelectNodes("fiyat").Item(0).InnerText.Replace(".", ",")
			Dim IndirimsizAlisfiyati As String = dokuman.Item("urunler").ChildNodes(i).SelectNodes("IndirimTutari").Item(0).InnerText.Replace(".", ",")
			Dim miktar As String = dokuman.Item("urunler").ChildNodes(i).SelectNodes("stoklar").Item(0).ChildNodes(0).SelectNodes("miktar").Item(0).InnerText
			Dim Stokkodu As Integer = 0
			'Ürünün varolup olmadığını kontrol et (Barkod Nosunu Baz Al)
			Dim cmdstr As String = "select * from URUNLER where TedarikciFirma='" + TedarikciFirma + "' and BarkodNo='" + BarkodKodu + "'"
			Dim dtUrun As DataTable = SaydanClass._ConnDB(cmdstr)
			Dim drUrun As DataRow
			Dim DegisiklikVarmi As Boolean = False
			If dtUrun.Rows.Count > 0 Then
				drUrun = dtUrun.Rows(0)
				StokKodu = drUrun("StokKodu").ToString
			Else
				drUrun = dtUrun.NewRow
				DegisiklikVarmi = True
			End If
			If drUrun("StokAdi1").ToString <> StokAdi Then drUrun("StokAdi1") = StokAdi : DegisiklikVarmi = True
			If drUrun("DetayAciklama1").ToString <> Aciklama Then drUrun("DetayAciklama1") = Aciklama : DegisiklikVarmi = True
			If drUrun("BarkodNo").ToString <> BarkodKodu Then drUrun("BarkodNo") = BarkodKodu : DegisiklikVarmi = True
			If drUrun("Pic1").ToString <> Resim1 Then drUrun("Pic1") = Resim1 : DegisiklikVarmi = True
			If drUrun("Pic2").ToString <> Resim2 Then drUrun("Pic2") = Resim2 : DegisiklikVarmi = True
			If drUrun("Pic3").ToString <> Resim3 Then drUrun("Pic3") = Resim3 : DegisiklikVarmi = True
			drUrun("KDVOrani") = 18
			drUrun("TedarikciFirma") = TedarikciFirma
			If IsDBNull(drUrun("IndirimsizAlisFiyati")) Then drUrun("IndirimsizAlisFiyati") = 0
			If IsDBNull(drUrun("AlisFiyati")) Then drUrun("AlisFiyati") = 0
			If IsDBNull(drUrun("SatisFiyati")) Then drUrun("SatisFiyati") = 0
			If drUrun("IndirimsizAlisFiyati") <> IndirimsizAlisFiyati Then drUrun("IndirimsizAlisFiyati") = IndirimsizAlisFiyati : DegisiklikVarmi = True
			If drUrun("AlisFiyati") <> AlisFiyati Then drUrun("AlisFiyati") = AlisFiyati : DegisiklikVarmi = True
			drUrun("AlisFiyatiPB") = 1
			If drUrun("Miktar").ToString <> Miktar Then drUrun("Miktar") = Miktar : DegisiklikVarmi = True

			Dim SatisFiyati As Decimal = AlisFiyati * 1.2


			If drUrun("KisaAciklama1").ToString <> StokAdi Then drUrun("KisaAciklama1") = StokAdi : DegisiklikVarmi = True
			If drUrun("SatisFiyati").ToString <> SatisFiyati Then drUrun("SatisFiyati") = SatisFiyati : DegisiklikVarmi = True
			drUrun("SatisFiyatiPB") = 1
			If drUrun("Keywords").ToString <> StokAdi Then drUrun("Keywords") = StokAdi + "," + Kategori : DegisiklikVarmi = True
			If drUrun("Description").ToString <> StokAdi Then drUrun("Description") = StokAdi : DegisiklikVarmi = True
			drUrun("GuncellemeTarihi") = GuncellemeTarihi
			drUrun("AktifMi") = True

			If dtUrun.Rows.Count = 0 Then dtUrun.Rows.Add(drUrun)
			If DegisiklikVarmi Then SaydanClass._SaveDB(cmdstr, dtUrun)

			If StokKodu = 0 Then
				StokKodu = SaydanClass._ConnDB(cmdstr).Rows(0)("StokKodu").ToString
			End If
			'4 kategorinin de sistemde tanımlı olup olmadığını kontrol et, değilse tanımla
			Dim KategoriKodu As Integer = 0
			cmdstr = "select * from [dbo].[KATEGORILER] where [XMLDegeri]='" + Kategori + "'"
			Dim dtKategori As DataTable = SaydanClass._ConnDB(cmdstr)
			If dtKategori.Rows.Count = 0 Then
				Dim drKategori = dtKategori.NewRow
				drKategori("StokGrupAdi1") = Kategori
				drKategori("XMLDegeri") = Kategori
				drKategori("Siralama") = 0
				dtKategori.Rows.Add(drKategori)
				SaydanClass._SaveDB(cmdstr, dtKategori)
				KategoriKodu = SaydanClass._ConnDB(cmdstr).Rows(0)("KategoriKodu").ToString
			Else
				KategoriKodu = dtKategori.Rows(0)("KategoriKodu")
			End If

			cmdstr = "select * from [dbo].[KATEGORILER_URUNLER] where KategoriKodu=" + KategoriKodu.ToString + " and UrunKodu=" + StokKodu.ToString
			Dim dtKategoriUrun As DataTable = SaydanClass._ConnDB(cmdstr)
			If dtKategoriUrun.Rows.Count = 0 Then
				Dim drKategoriUrun As DataRow = dtKategoriUrun.NewRow
				drKategoriUrun("KategoriKodu") = KategoriKodu
				drKategoriUrun("UrunKodu") = StokKodu
				dtKategoriUrun.Rows.Add(drKategoriUrun)
				SaydanClass._SaveDB(cmdstr, dtKategoriUrun)
			End If

			cmdhtml += vbCrLf + "<tr><td>" + (i + 1).ToString + "</td><td>" + BarkodKodu + "</td><td>" + StokAdi + "</td> <td> " + Kategori + "</td<td> " + AlisFiyati + "</td><td> " + IndirimsizAlisFiyati + "</td><td>18</td><td>" + Miktar + "</td><td><img src=""" + Resim1 + """ width=""50"" alt=""" + StokAdi + """ /></td><td><img src=""" + Resim2 + """ width=""50"" alt=""" + StokAdi + """ /></td><td><img src=""" + Resim3 + """ width=""50"" alt=""" + StokAdi + """ /></td></tr>"


		Next
		Dim cmdstr2 As String = "UPDATE URUNLER SET AktifMi=0 where TedarikciFirma='" + TedarikciFirma + "' and GuncellemeTarihi <> '" + GuncellemeTarihi + "'"
		SaydanClass.ExecSQL(cmdstr2)
		cmdhtml += vbCrLf + " </tbody></table>"
		spn1.InnerHtml = cmdhtml
	End Sub

	Protected Sub N11_comdanYukle(ByVal XMLAdresi As String, ByVal TedarikciFirma As String)
		okuyucu = New XmlTextReader(XMLAdresi)
		dokuman = New XmlDocument
		Try
			dokuman.Load(okuyucu)
		Catch ex As Exception
			Exit Sub
		End Try
		Dim GuncellemeTarihi As String = Now.ToString("dd.MM.yyyy HH:mm:ss")
		Dim cmdhtml As String = "<table style=""padding:10px; border-collapse:collapse;"" border=""1""><thead><tr><td>#</td><td>BarkodNo</td><td>Ürün Adı</td> <td> Kategori1</td><td> Alış fiyatı</td><td> İndirimsiz Fiyat</td><td> KDV Oranı</td><td>Miktar </td><td>Resim1</td><td>Resim2</td><td>Resim3</td></tr></thead><tbody>"

		For i = 0 To dokuman.Item("Urunler").ChildNodes.Count - 1
			Dim StokAdi As String = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Baslik").Item(0).InnerText
			Dim Resim1 As String = ""
			Dim Resim2 As String = ""
			Dim Resim3 As String = ""
			Dim Resim4 As String = ""
			Dim Resim5 As String = ""
			If Not dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Resimler").Item(0).ChildNodes(0) Is Nothing Then Resim1 = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Resimler").Item(0).ChildNodes(0).InnerText
			If Not dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Resimler").Item(0).ChildNodes(1) Is Nothing Then Resim2 = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Resimler").Item(0).ChildNodes(1).InnerText
			If Not dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Resimler").Item(0).ChildNodes(2) Is Nothing Then Resim3 = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Resimler").Item(0).ChildNodes(2).InnerText
			If Not dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Resimler").Item(0).ChildNodes(3) Is Nothing Then Resim4 = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Resimler").Item(0).ChildNodes(3).InnerText
			If Not dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Resimler").Item(0).ChildNodes(4) Is Nothing Then Resim5 = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Resimler").Item(0).ChildNodes(4).InnerText
			Dim Kategori As String = ""
			'Dim Kategori2 As String = ""
			'Dim Kategori3 As String = ""
			'Dim Kategori4 As String = ""
			If Not dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Kategori").Item(0).Attributes("isim").Value Is Nothing Then Kategori = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Kategori").Item(0).Attributes("isim").Value
			Dim BarkodKodu As String = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Kod").Item(0).InnerText
			Dim Aciklama As String = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Aciklama").Item(0).InnerText
			Dim AlisFiyati As String = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Fiyat").Item(0).InnerText.Replace(".", ",")
			Dim IndirimsizAlisFiyati As String = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("IndirimTutari").Item(0).InnerText.Replace(".", ",")
			Dim Miktar As String = dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Stoklar").Item(0).ChildNodes(0).SelectNodes("Miktar").Item(0).InnerText
			Dim StokKodu As Integer = 0
			'Ürünün varolup olmadığını kontrol et (Barkod Nosunu Baz Al)
			Dim cmdstr As String = "select * from URUNLER where TedarikciFirma='" + TedarikciFirma + "' and BarkodNo='" + BarkodKodu + "'"
			Dim dtUrun As DataTable = SaydanClass._ConnDB(cmdstr)
			Dim drUrun As DataRow
			Dim DegisiklikVarmi As Boolean = False
			If dtUrun.Rows.Count > 0 Then
				drUrun = dtUrun.Rows(0)
				StokKodu = drUrun("StokKodu").ToString
			Else
				drUrun = dtUrun.NewRow
				DegisiklikVarmi = True
			End If
			If drUrun("StokAdi1").ToString <> StokAdi Then drUrun("StokAdi1") = StokAdi : DegisiklikVarmi = True
			If drUrun("DetayAciklama1").ToString <> Aciklama Then drUrun("DetayAciklama1") = Aciklama : DegisiklikVarmi = True
			If drUrun("BarkodNo").ToString <> BarkodKodu Then drUrun("BarkodNo") = BarkodKodu : DegisiklikVarmi = True
			If drUrun("Pic1").ToString <> Resim1 Then drUrun("Pic1") = Resim1 : DegisiklikVarmi = True
			If drUrun("Pic2").ToString <> Resim2 Then drUrun("Pic2") = Resim2 : DegisiklikVarmi = True
			If drUrun("Pic3").ToString <> Resim3 Then drUrun("Pic3") = Resim3 : DegisiklikVarmi = True
			drUrun("KDVOrani") = 18
			drUrun("TedarikciFirma") = TedarikciFirma
			If IsDBNull(drUrun("IndirimsizAlisFiyati")) Then drUrun("IndirimsizAlisFiyati") = 0
			If IsDBNull(drUrun("AlisFiyati")) Then drUrun("AlisFiyati") = 0
			If IsDBNull(drUrun("SatisFiyati")) Then drUrun("SatisFiyati") = 0
			If drUrun("IndirimsizAlisFiyati") <> IndirimsizAlisFiyati Then drUrun("IndirimsizAlisFiyati") = IndirimsizAlisFiyati : DegisiklikVarmi = True
			If drUrun("AlisFiyati") <> AlisFiyati Then drUrun("AlisFiyati") = AlisFiyati : DegisiklikVarmi = True
			drUrun("AlisFiyatiPB") = 1
			If drUrun("Miktar").ToString <> Miktar Then drUrun("Miktar") = Miktar : DegisiklikVarmi = True

			Dim SatisFiyati As Decimal = AlisFiyati * 1.2


			If drUrun("KisaAciklama1").ToString <> StokAdi Then drUrun("KisaAciklama1") = StokAdi : DegisiklikVarmi = True
			If drUrun("SatisFiyati").ToString <> SatisFiyati Then drUrun("SatisFiyati") = SatisFiyati : DegisiklikVarmi = True
			drUrun("SatisFiyatiPB") = 1
			If drUrun("Keywords").ToString <> StokAdi Then drUrun("Keywords") = StokAdi + "," + Kategori : DegisiklikVarmi = True
			If drUrun("Description").ToString <> StokAdi Then drUrun("Description") = StokAdi : DegisiklikVarmi = True
			drUrun("GuncellemeTarihi") = GuncellemeTarihi
			drUrun("AktifMi") = True

			If dtUrun.Rows.Count = 0 Then dtUrun.Rows.Add(drUrun)
			If DegisiklikVarmi Then SaydanClass._SaveDB(cmdstr, dtUrun)

			If StokKodu = 0 Then
				StokKodu = SaydanClass._ConnDB(cmdstr).Rows(0)("StokKodu").ToString
			End If
			'4 kategorinin de sistemde tanımlı olup olmadığını kontrol et, değilse tanımla
			Dim KategoriKodu As Integer = 0
			cmdstr = "select * from [dbo].[KATEGORILER] where [XMLDegeri]='" + Kategori + "'"
			Dim dtKategori As DataTable = SaydanClass._ConnDB(cmdstr)
			If dtKategori.Rows.Count = 0 Then
				Dim drKategori = dtKategori.NewRow
				drKategori("StokGrupAdi1") = Kategori
				drKategori("XMLDegeri") = Kategori
				drKategori("Siralama") = 0
				dtKategori.Rows.Add(drKategori)
				SaydanClass._SaveDB(cmdstr, dtKategori)
				KategoriKodu = SaydanClass._ConnDB(cmdstr).Rows(0)("KategoriKodu").ToString
			Else
				KategoriKodu = dtKategori.Rows(0)("KategoriKodu")
			End If

			cmdstr = "select * from [dbo].[KATEGORILER_URUNLER] where KategoriKodu=" + KategoriKodu.ToString + " and UrunKodu=" + StokKodu.ToString
			Dim dtKategoriUrun As DataTable = SaydanClass._ConnDB(cmdstr)
			If dtKategoriUrun.Rows.Count = 0 Then
				Dim drKategoriUrun As DataRow = dtKategoriUrun.NewRow
				drKategoriUrun("KategoriKodu") = KategoriKodu
				drKategoriUrun("UrunKodu") = StokKodu
				dtKategoriUrun.Rows.Add(drKategoriUrun)
				SaydanClass._SaveDB(cmdstr, dtKategoriUrun)
			End If

			'cmdhtml += vbCrLf + "<tr><td>" + (i + 1).ToString + "</td><td>" + BarkodKodu + "</td><td>" + StokAdi + "</td> <td> " + Kategori + "</td><td> " + AlisFiyati + "</td><td> " + IndirimsizAlisFiyati + "</td><td>18</td><td>" + Miktar + "</td><td><img src=""" + Resim1 + """ width=""50"" alt=""" + StokAdi + """ /></td><td><img src=""" + Resim2 + """ width=""50"" alt=""" + StokAdi + """ /></td><td><img src=""" + Resim3 + """ width=""50"" alt=""" + StokAdi + """ /></td></tr>"


		Next
		Dim cmdstr2 As String = "UPDATE URUNLER SET AktifMi=0 where TedarikciFirma='" + TedarikciFirma + "' and GuncellemeTarihi <> '" + GuncellemeTarihi + "'"
		SaydanClass.ExecSQL(cmdstr2)
		'cmdhtml += vbCrLf + " </tbody></table>"
		'spn1.InnerHtml = cmdhtml
	End Sub


	Protected Sub N11_com_XMLinde_FiyatGuncelle(ByVal XMLAdresi As String, ByVal TedarikciFirma As String)
		okuyucu = New XmlTextReader(XMLAdresi)
		dokuman = New XmlDocument
		Try
			dokuman.Load(okuyucu)
		Catch ex As Exception
			Exit Sub
		End Try

		For i = 0 To dokuman.Item("Urunler").ChildNodes.Count - 1

			Dim AlisFiyati As Decimal = Convert.ToDecimal(dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Fiyat").Item(0).InnerText.Replace(".", ","))
			Dim SatisFiyati As Decimal = AlisFiyati * 1.3
			dokuman.Item("Urunler").ChildNodes(i).SelectNodes("Fiyat").Item(0).InnerText = Replace(SatisFiyati, ",", ".")
		Next

		dokuman.Save(Server.MapPath("./" + TedarikciFirma + ".xml"))
	End Sub


End Class
