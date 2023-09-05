Imports System.Data
Partial Class SiparisListe
	Inherits System.Web.UI.Page

	Dim Saydanclass As New SaydanClass
	Dim KontrolOK As Boolean = True
	Dim SiparisKodu As Integer = 0

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		OdemesiTamamlanmisSiparisler()
		TedarikciyeYonlendirilmisSiparisler()
		KargoyaVerilenSiparisler()
		IadeEdilenSiparisler()
	End Sub


	Protected Sub IadeEdilenSiparisler()
		Dim cmdstr As String = "SELECT SIPARISLER.SiparisKodu, SIPARISLER.MusteriKodu, ISNULL(SIPARISLER.OdemeYapildiMi, 0) AS OdemeYapildiMi, SIPARISLER.TeslimatSehri, ODEME_ISLEMLERI.PaidPrice, ODEME_ISLEMLERI.PaymentStatus, ODEME_ISLEMLERI.KayitTarihi, URUNLER.TedarikciFirma, STOK_HAREKET.Tutar AS SatisFiyati, STOK_HAREKET.AlisFiyati, URUNLER.StokAdi1, MUSTERILER.Isim, MUSTERILER.Telefon, MUSTERILER.Soyisim, SIPARISLER.TedarikTamamlamaTarihi, SIPARISLER.KargoyaTeslimTarihi, SIPARISLER.MusteriyeTeslimTarihi, ODEME_ISLEMLERI.IadeMi, ODEME_ISLEMLERI.IadeSebebi, ODEME_ISLEMLERI.IadeGerceklesmeTarihi FROM SIPARISLER INNER JOIN (SELECT Kod, SiparisKodu, Token, Status, PaymentStatus, PaidPrice, ErrorMessages, BasketID, BinNumber, CardAssociation, CardFamily, PaymentID, SystemTime, KayitTarihi, IadeMi, IadeSebebi, IadeGerceklesmeTarihi FROM ODEME_ISLEMLERI AS ODEME_ISLEMLERI_1 WHERE (PaymentStatus = 'SUCCESS')) AS ODEME_ISLEMLERI ON SIPARISLER.SiparisKodu = ODEME_ISLEMLERI.SiparisKodu INNER JOIN STOK_HAREKET ON SIPARISLER.SiparisKodu = STOK_HAREKET.SiparisKodu INNER JOIN URUNLER ON STOK_HAREKET.UrunKodu = URUNLER.StokKodu INNER JOIN MUSTERILER ON SIPARISLER.MusteriKodu = MUSTERILER.MusteriKodu WHERE (ODEME_ISLEMLERI.IadeMi=1) and ODEME_ISLEMLERI.IadeGerceklesmeTarihi is null"
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim whrcnd As String = ""
		Dim SiraNo As Integer = 0
		Dim hr As New HtmlTableRow
		Dim hc As New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Sipariş No" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Müşteri Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ödeme Tarihi" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürünler" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Tedarikçi Firma" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Satış Fiyatı" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Alış Fiyatı" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Sipariş Durumu" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "İade Sebebi" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "İşlem" : hr.Cells.Add(hc)
		tblIadeEdilenSiparisler.Rows.Add(hr)
		Dim AraToplam As Decimal = 0
		'lblAdet.Text = dt.Rows.Count.ToString
		If dt.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 10
			hc.InnerHtml = "Tedarikçiye gönderilmeyi bekleyen ürün bulunmuyor."
			hr.Cells.Add(hc)
			tblIadeEdilenSiparisler.Rows.Add(hr)
			Exit Sub
		End If

		Dim Urunler As String = ""
		Dim SatisFiyati As Decimal = 0
		Dim AlisFiyati As Decimal = 0
		For SiraNo = 0 To dt.Rows.Count - 1
			Dim dr As DataRow = dt.Rows(0)

			If Urunler = "" Then
				Urunler = Mid(dr("StokAdi1").ToString, 1, 25)
			Else
				Urunler += "<br />" + Mid(dr("StokAdi1").ToString, 1, 25)
			End If
			SatisFiyati += dr("SatisFiyati")
			If IsDBNull(dr("AlisFiyati")) Then dr("AlisFiyati") = 0
			AlisFiyati += dr("AlisFiyati")

			Dim SiparişDurumu As String = ""
			If IsDBNull(dr("TedarikTamamlamaTarihi")) Then
				SiparişDurumu = "Ödeme Tamamlandı"
			ElseIf IsDBNull(dr("KargoyaTeslimTarihi")) Then
				SiparişDurumu = "Tedarikçiye Sipariş Gönderildi"
			ElseIf IsDBNull(dr("MusteriyeTeslimTarihi")) Then
				SiparişDurumu = "Kargoya Verildi"
			ElseIf Not IsDBNull(dr("MusteriyeTeslimTarihi")) Then
				SiparişDurumu = "Müşteriye Teslim Edildi"
			End If

			If SiraNo = dt.Rows.Count - 1 Then
				hr = New HtmlTableRow
				hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("SiparisKodu").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("Isim").ToString + " " + dr("Soyisim").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<nobr>" + CDate(dr("KayitTarihi")).ToString("dd.MM.yyyy HH:mm") + "</nobr>" : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = Urunler : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("TedarikciFirma").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = SatisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = AlisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = SiparişDurumu : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("IadeSebebi").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<a href=""SiparisDetay2.aspx?ted=" + dr("TedarikciFirma").ToString + "&kod=" + dr("SiparisKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
				tblIadeEdilenSiparisler.Rows.Add(hr)
			ElseIf dr("SiparisKodu") <> dt.Rows(SiraNo + 1)("SiparisKodu") And dr("TedarikciFirma") <> dt.Rows(SiraNo + 1)("TedarikciFirma") Then
				hr = New HtmlTableRow
				hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("SiparisKodu").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("Isim").ToString + " " + dr("Soyisim").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<nobr>" + CDate(dr("KayitTarihi")).ToString("dd.MM.yyyy HH:mm") + "</nobr>" : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = Urunler : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("TedarikciFirma").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = SatisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = AlisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = (SatisFiyati - AlisFiyati).ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<a href=""SiparisDetay2.aspx?ted=" + dr("TedarikciFirma").ToString + "&kod=" + dr("SiparisKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
				tblIadeEdilenSiparisler.Rows.Add(hr)
				Urunler = ""
				SatisFiyati = 0
				AlisFiyati = 0
			End If
		Next


	End Sub


	Protected Sub OdemesiTamamlanmisSiparisler()




		Dim cmdstr As String = "SELECT SIPARISLER.SiparisKodu, SIPARISLER.MusteriKodu, ISNULL(SIPARISLER.OdemeYapildiMi, 0) AS OdemeYapildiMi, SIPARISLER.TeslimatSehri, ODEME_ISLEMLERI.PaidPrice, ODEME_ISLEMLERI.PaymentStatus, ODEME_ISLEMLERI.KayitTarihi, URUNLER.TedarikciFirma, STOK_HAREKET.Tutar AS SatisFiyati, STOK_HAREKET.AlisFiyati, URUNLER.StokAdi1, MUSTERILER.Isim, MUSTERILER.Telefon, MUSTERILER.Soyisim FROM SIPARISLER INNER JOIN (SELECT * FROM ODEME_ISLEMLERI AS ODEME_ISLEMLERI_1 WHERE (PaymentStatus = 'SUCCESS')) AS ODEME_ISLEMLERI ON SIPARISLER.SiparisKodu = ODEME_ISLEMLERI.SiparisKodu INNER JOIN STOK_HAREKET ON SIPARISLER.SiparisKodu = STOK_HAREKET.SiparisKodu INNER JOIN URUNLER ON STOK_HAREKET.UrunKodu = URUNLER.StokKodu INNER JOIN MUSTERILER ON SIPARISLER.MusteriKodu = MUSTERILER.MusteriKodu WHERE (SIPARISLER.TedarikTamamlamaTarihi IS NULL) and (ODEME_ISLEMLERI.IadeMi is null or ODEME_ISLEMLERI.IadeMi=0)"
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim whrcnd As String = ""
		Dim SiraNo As Integer = 0
		Dim hr As New HtmlTableRow
		Dim hc As New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Sipariş No" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Müşteri Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ödeme Tarihi" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürünler" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Tedarikçi Firma" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Satış Fiyatı" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Alış Fiyatı" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Kâr" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "İşlem" : hr.Cells.Add(hc)
		tblOdemesiYapilanlar.Rows.Add(hr)
		Dim AraToplam As Decimal = 0
		'lblAdet.Text = dt.Rows.Count.ToString
		If dt.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 7
			hc.InnerHtml = "Tedarikçiye gönderilmeyi bekleyen ürün bulunmuyor."
			hr.Cells.Add(hc)
			tblOdemesiYapilanlar.Rows.Add(hr)
			Exit Sub
		End If

		Dim Urunler As String = ""
		Dim SatisFiyati As Decimal = 0
		Dim AlisFiyati As Decimal = 0
		For SiraNo = 0 To dt.Rows.Count - 1
			Dim dr As DataRow = dt.Rows(0)

			If Urunler = "" Then
				Urunler = Mid(dr("StokAdi1").ToString, 1, 25)
			Else
				Urunler += "<br />" + Mid(dr("StokAdi1").ToString, 1, 25)
			End If
			If Not IsDBNull(dr("SatisFiyati")) Then SatisFiyati += dr("SatisFiyati")
			If Not IsDBNull(dr("AlisFiyati")) Then AlisFiyati += dr("AlisFiyati")

			If SiraNo = dt.Rows.Count - 1 Then
				hr = New HtmlTableRow
				hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("SiparisKodu").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("Isim").ToString + " " + dr("Soyisim").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<nobr>" + CDate(dr("KayitTarihi")).ToString("dd.MM.yyyy HH:mm") + "</nobr>" : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = Urunler : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("TedarikciFirma").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = SatisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = AlisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = (SatisFiyati - AlisFiyati).ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<a href=""SiparisDetayYonetici.aspx?ted=" + dr("TedarikciFirma").ToString + "&kod=" + dr("SiparisKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
				tblOdemesiYapilanlar.Rows.Add(hr)
			ElseIf dr("SiparisKodu") <> dt.Rows(SiraNo + 1)("SiparisKodu") And dr("TedarikciFirma") <> dt.Rows(SiraNo + 1)("TedarikciFirma") Then
				hr = New HtmlTableRow
				hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("SiparisKodu").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("Isim").ToString + " " + dr("Soyisim").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<nobr>" + CDate(dr("KayitTarihi")).ToString("dd.MM.yyyy HH:mm") + "</nobr>" : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = Urunler : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("TedarikciFirma").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = SatisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = AlisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = (SatisFiyati - AlisFiyati).ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<a href=""SiparisDetayYonetici.aspx?ted=" + dr("TedarikciFirma").ToString + "&kod=" + dr("SiparisKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
				tblOdemesiYapilanlar.Rows.Add(hr)
				Urunler = ""
				SatisFiyati = 0
				AlisFiyati = 0
			End If
		Next


	End Sub


	Protected Sub TedarikciyeYonlendirilmisSiparisler()
		Dim cmdstr As String = "SELECT        SIPARISLER.SiparisKodu, SIPARISLER.MusteriKodu, ISNULL(SIPARISLER.OdemeYapildiMi, 0) AS OdemeYapildiMi, SIPARISLER.TeslimatSehri, URUNLER.TedarikciFirma, STOK_HAREKET.Tutar AS SatisFiyati, STOK_HAREKET.AlisFiyati, URUNLER.StokAdi1, MUSTERILER.Isim, MUSTERILER.Telefon, MUSTERILER.Soyisim, SIPARISLER.TedarikTamamlamaTarihi FROM            SIPARISLER INNER JOIN STOK_HAREKET ON SIPARISLER.SiparisKodu = STOK_HAREKET.SiparisKodu INNER JOIN URUNLER ON STOK_HAREKET.UrunKodu = URUNLER.StokKodu INNER JOIN MUSTERILER ON SIPARISLER.MusteriKodu = MUSTERILER.MusteriKodu INNER JOIN ODEME_ISLEMLERI ON SIPARISLER.SiparisKodu = ODEME_ISLEMLERI.SiparisKodu WHERE        (SIPARISLER.TedarikTamamlamaTarihi IS NOT NULL) AND (SIPARISLER.KargoyaTeslimTarihi IS NULL) and (ODEME_ISLEMLERI.IadeMi is null or ODEME_ISLEMLERI.IadeMi=0)"
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim whrcnd As String = ""
		Dim SiraNo As Integer = 0
		Dim hr As New HtmlTableRow
		Dim hc As New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Sipariş No" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Müşteri Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Gönderim Tarihi" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürünler" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Tedarikçi Firma" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Satış Fiyatı" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Alış Fiyatı" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Kâr" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "İşlem" : hr.Cells.Add(hc)
		tblTedarikciyeYonlendirilenler.Rows.Add(hr)
		Dim AraToplam As Decimal = 0
		'lblAdet.Text = dt.Rows.Count.ToString
		If dt.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 7
			hc.InnerHtml = "Tedarikçiye gönderilmeyi bekleyen ürün bulunmuyor."
			hr.Cells.Add(hc)
			tblTedarikciyeYonlendirilenler.Rows.Add(hr)
			Exit Sub
		End If

		Dim Urunler As String = ""
		Dim SatisFiyati As Decimal = 0
		Dim AlisFiyati As Decimal = 0
		For SiraNo = 0 To dt.Rows.Count - 1
			Dim dr As DataRow = dt.Rows(0)

			If Urunler = "" Then
				Urunler = Mid(dr("StokAdi1").ToString, 1, 25)
			Else
				Urunler += "<br />" + Mid(dr("StokAdi1").ToString, 1, 25)
			End If
			If Not IsDBNull(dr("SatisFiyati")) Then SatisFiyati += dr("SatisFiyati")
			If Not IsDBNull(dr("AlisFiyati")) Then AlisFiyati += dr("AlisFiyati")

			If SiraNo = dt.Rows.Count - 1 Then
				hr = New HtmlTableRow
				hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("SiparisKodu").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("Isim").ToString + " " + dr("Soyisim").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<nobr>" + CDate(dr("TedarikTamamlamaTarihi")).ToString("dd.MM.yyyy HH:mm") + "</nobr>" : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = Urunler : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("TedarikciFirma").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = SatisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = AlisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = (SatisFiyati - AlisFiyati).ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<a href=""SiparisDetayYonetici.aspx?ted=" + dr("TedarikciFirma").ToString + "&kod=" + dr("SiparisKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
				tblTedarikciyeYonlendirilenler.Rows.Add(hr)
			ElseIf dr("SiparisKodu") <> dt.Rows(SiraNo + 1)("SiparisKodu") And dr("TedarikciFirma") <> dt.Rows(SiraNo + 1)("TedarikciFirma") Then
				hr = New HtmlTableRow
				hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("SiparisKodu").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("Isim").ToString + " " + dr("Soyisim").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<nobr>" + CDate(dr("TedarikTamamlamaTarihi")).ToString("dd.MM.yyyy HH:mm") + "</nobr>" : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = Urunler : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("TedarikciFirma").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = SatisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = AlisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = (SatisFiyati - AlisFiyati).ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<a href=""SiparisDetayYonetici.aspx?ted=" + dr("TedarikciFirma").ToString + "&kod=" + dr("SiparisKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
				tblTedarikciyeYonlendirilenler.Rows.Add(hr)
				Urunler = ""
				SatisFiyati = 0
				AlisFiyati = 0
			End If
		Next


	End Sub

	Protected Sub KargoyaVerilenSiparisler()
		Dim cmdstr As String = "SELECT        SIPARISLER.SiparisKodu, SIPARISLER.MusteriKodu, ISNULL(SIPARISLER.OdemeYapildiMi, 0) AS OdemeYapildiMi, SIPARISLER.TeslimatSehri, URUNLER.TedarikciFirma, STOK_HAREKET.Tutar AS SatisFiyati, STOK_HAREKET.AlisFiyati, URUNLER.StokAdi1, MUSTERILER.Isim, MUSTERILER.Telefon, MUSTERILER.Soyisim, SIPARISLER.KargoyaTeslimTarihi, SIPARISLER.KargoFirmasi, SIPARISLER.KargoNo FROM            SIPARISLER INNER JOIN STOK_HAREKET ON SIPARISLER.SiparisKodu = STOK_HAREKET.SiparisKodu INNER JOIN URUNLER ON STOK_HAREKET.UrunKodu = URUNLER.StokKodu INNER JOIN MUSTERILER ON SIPARISLER.MusteriKodu = MUSTERILER.MusteriKodu INNER JOIN ODEME_ISLEMLERI ON SIPARISLER.SiparisKodu = ODEME_ISLEMLERI.SiparisKodu WHERE        (SIPARISLER.KargoyaTeslimTarihi IS NOT NULL) AND (SIPARISLER.MusteriyeTeslimTarihi IS NULL) and (ODEME_ISLEMLERI.IadeMi is null or ODEME_ISLEMLERI.IadeMi =0)"
		Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
		Dim whrcnd As String = ""
		Dim SiraNo As Integer = 0
		Dim hr As New HtmlTableRow
		Dim hc As New HtmlTableCell : hc.InnerHtml = "#" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Sipariş No" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Müşteri Adı" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Kargo Tarihi" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Kargo Firması" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Kargo No" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Ürünler" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Tedarikçi Firma" : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Satış Fiyatı" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Alış Fiyatı" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "Kâr" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
		hc = New HtmlTableCell : hc.InnerHtml = "İşlem" : hr.Cells.Add(hc)
		tblKargoyaVerilenler.Rows.Add(hr)
		Dim AraToplam As Decimal = 0
		'lblAdet.Text = dt.Rows.Count.ToString
		If dt.Rows.Count = 0 Then
			hr = New HtmlTableRow
			hc = New HtmlTableCell
			hc.ColSpan = 7
			hc.InnerHtml = "Tedarikçiye gönderilmeyi bekleyen ürün bulunmuyor."
			hr.Cells.Add(hc)
			tblKargoyaVerilenler.Rows.Add(hr)
			Exit Sub
		End If

		Dim Urunler As String = ""
		Dim SatisFiyati As Decimal = 0
		Dim AlisFiyati As Decimal = 0
		For SiraNo = 0 To dt.Rows.Count - 1
			Dim dr As DataRow = dt.Rows(0)

			If Urunler = "" Then
				Urunler = Mid(dr("StokAdi1").ToString, 1, 25)
			Else
				Urunler += "<br />" + Mid(dr("StokAdi1").ToString, 1, 25)
			End If
			If Not IsDBNull(dr("SatisFiyati")) Then SatisFiyati += dr("SatisFiyati")
			If Not IsDBNull(dr("AlisFiyati")) Then AlisFiyati += dr("AlisFiyati")

			If SiraNo = dt.Rows.Count - 1 Then
				hr = New HtmlTableRow
				hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("SiparisKodu").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("Isim").ToString + " " + dr("Soyisim").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<nobr>" + CDate(dr("KargoyaTeslimTarihi")).ToString("dd.MM.yyyy HH:mm") + "</nobr>" : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("KargoFirmasi").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("KargoNo").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = Urunler : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("TedarikciFirma").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = SatisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = AlisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = (SatisFiyati - AlisFiyati).ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<a href=""SiparisDetayYonetici.aspx?ted=" + dr("TedarikciFirma").ToString + "&kod=" + dr("SiparisKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
				tblKargoyaVerilenler.Rows.Add(hr)
			ElseIf dr("SiparisKodu") <> dt.Rows(SiraNo + 1)("SiparisKodu") And dr("TedarikciFirma") <> dt.Rows(SiraNo + 1)("TedarikciFirma") Then
				hr = New HtmlTableRow
				hc = New HtmlTableCell : hc.InnerHtml = (SiraNo + 1).ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("SiparisKodu").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("Isim").ToString + " " + dr("Soyisim").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<nobr>" + CDate(dr("KargoyaTeslimTarihi")).ToString("dd.MM.yyyy HH:mm") + "</nobr>" : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("KargoFirmasi").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("KargoNo").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = Urunler : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = dr("TedarikciFirma").ToString : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = SatisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = AlisFiyati.ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = (SatisFiyati - AlisFiyati).ToString("n2") + " TL" : hc.Style.Add("text-align", "right") : hr.Cells.Add(hc)
				hc = New HtmlTableCell : hc.InnerHtml = "<a href=""SiparisDetayYonetici.aspx?ted=" + dr("TedarikciFirma").ToString + "&kod=" + dr("SiparisKodu").ToString + """ target=""_blank"">Detay</a>" : hr.Cells.Add(hc)
				tblKargoyaVerilenler.Rows.Add(hr)
				Urunler = ""
				SatisFiyati = 0
				AlisFiyati = 0
			End If
		Next


	End Sub


	Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		Saydanclass.LoginKontroluYap()
		If Not Saydanclass.YoneticiMi Then Response.Redirect("SiparisListe.aspx")
		'If Session("UserID") Is Nothing Then Response.Redirect("login.aspx")
		'If Session("YoneticiMi") Is Nothing Then Response.Redirect("SiparisListe.aspx")
		'If Session("YoneticiMi") <> True Then Response.Redirect("SiparisListe.aspx")
	End Sub


End Class
