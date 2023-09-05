<%@ Page Title="" Language="VB" ValidateRequest="false"  MasterPageFile="~/Master2.master" AutoEventWireup="false" CodeFile="UrunlerDetay.aspx.vb" Inherits="UrunlerDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<style>
		.muratozel label {
			display:inline ;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<div id="mainBody">
		<div class="container">
			<div class="row">
				<div class="span12">


					<ul class="breadcrumb">
						<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
						<li><a href="UyeIslemleri.aspx">Üye İşlemleri</a> <span class="divider">/</span></li>
						<li><a href="UrunlerListe.aspx">Ürün Listesi</a> <span class="divider">/</span></li>
						<li class="active">Ürün Detayı</li>
					</ul>
					<hr class="soft" />

					<div class="well">
						<div class="container-fluid ">
							<div class="row ">
							<h4>Ürün Detayı</h4>
							<div class="control-group"><label class="control-label" for="lblUrunKodu">Ürün Kodu</label>
								<div class="controls">
									<asp:Label ID="lblUrunKodu" runat="server">123</asp:Label>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtUrunAdi">Ürün Adı<sup>*</sup></label><div class="controls ">
									<asp:TextBox ID="txtUrunAdi" runat="server" width="100%"></asp:TextBox>
								</div>
							</div>
							<div class="control-group hidden "><label class="control-label" for="txtKisaAciklama">Kısa Açıklama<sup>*</sup></label><div class="controls">
									<asp:TextBox ID="txtKisaAciklama" runat="server" width="100%"   ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtBarkodNo">Barkod No</label><div class="controls">
									<asp:TextBox ID="txtBarkodNo" runat="server"  width="100%" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtTedarikciFirma">Tedarikçi Firma</label><div class="controls">
									<asp:TextBox ID="txtTedarikciFirma" runat="server" width="100%" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtDetay">Miktar</label><div class="controls">
									<asp:TextBox ID="txtMiktar" runat="server" width="100%" ></asp:TextBox>
								</div>
							</div>

							<div class="control-group "><label class="control-label" for="txtAlisFiyati">Alış Fiyati</label><div class="controls">
									<asp:TextBox ID="txtAlisFiyati" runat="server"  width="100%" />
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtSatisFiyati">Satış Faturası</label><div class="controls">
									<asp:TextBox ID="txtSatisFiyati" runat="server" width="100%" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtTedarikciFirma">Resim1</label><div class="controls">
									<asp:TextBox ID="txtResim1" runat="server" width="100%" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtTedarikciFirma">Resim2</label><div class="controls">
									<asp:TextBox ID="txtResim2" runat="server" width="100%" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtTedarikciFirma">Resim3</label><div class="controls">
									<asp:TextBox ID="txtResim3" runat="server" width="100%" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtTedarikciFirma">Resim4</label><div class="controls">
									<asp:TextBox ID="txtResim4" runat="server" width="100%" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtTedarikciFirma">Resim5</label><div class="controls">
									<asp:TextBox ID="txtResim5" runat="server" width="100%" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="drpAktifMi">Aktif Mi</label><div class="controls">
								<asp:DropDownList ID="drpAktifMi" runat="server">
									<asp:ListItem Value="True">Aktif</asp:ListItem>
									<asp:ListItem  Value="False">Pasif</asp:ListItem>
								</asp:DropDownList>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtUrunDetayi">Ürün Detayı</label><div class="controls">
									<asp:TextBox ID="txtUrunDetayi" runat="server" width="100%" TextMode="MultiLine" ValidateRequestMode="Enabled"  ></asp:TextBox>
								</div>
							</div>

							<div class="control-group "><label class="control-label" for="txtDescription">Meta Description</label><div class="controls">
									<asp:TextBox ID="txtDescription" runat="server" width="100%"  TextMode="MultiLine" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtKeyword">Meta Keywords</label><div class="controls">
									<asp:TextBox ID="txtKeyword" runat="server" width="100%"  TextMode="MultiLine" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="lblDetay">Detay (Görünüm)</label><div class="controls">
									<asp:Label ID="lblDetay" runat="server" width="100%" TextMode ="MultiLine" ></asp:Label>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="chkKategoriler">Kategoriler</label>
									<div class="controls">
<asp:CheckBoxList ID="chkKategoriler" runat="server" CssClass="muratozel" RepeatDirection="Vertical" RepeatLayout="OrderedList" RepeatColumns="1" Width="100%"></asp:CheckBoxList>
								</div>
							</div>





							<hr />

							<div class="alert alert-block alert-error fade in" id="divUyari" runat="server" visible="false">
								<asp:LinkButton CssClass="close" data-dismiss="alert" ID="btnUyari" runat="server">x</asp:LinkButton>
								<asp:Label ID="lblUyari" runat="server"></asp:Label>
							</div>



							<div class="control-group">
								<div class="controls">
									<%--				<input type="hidden" name="email_create" value="1">
				<input type="hidden" name="is_new_customer" value="1">--%>
									<asp:LinkButton ID="btnRegister" runat="server" CssClass="btn btn-large pull-right btn-default">Kaydet</asp:LinkButton>
									<asp:LinkButton ID="btnSil" runat="server" CssClass="btn btn-large pull-left btn-danger" Visible="false" >Sil</asp:LinkButton>
								</div>
							</div>
							<p><sup>*</sup>Girilmesi Zorunlu Alanlar</p>
						</div>

						</div>
					</div>

<%--<span id="spnUrunler" runat="server" >
			<hr class="soft"/>
			<h3>"<asp:Label id="lblKategoriAdi" runat="server" > </asp:Label>"&nbsp; Kategorisindeki Ürünler : </h3>
			<table class="table table-bordered" id="tblUrunler" runat="server"></table>


</span>--%>







				</div>
			</div>
		</div>
	</div>








</asp:Content>

