<%@ Page Title="" Language="VB" EnableEventValidation="true" MasterPageFile="~/Master2.master" AutoEventWireup="false" CodeFile="BannerDetay.aspx.vb" Inherits="BannerDetay" %>

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
						<li><a href="BannerListe.aspx">Bannerlar Listesi</a> <span class="divider">/</span></li>
						<li class="active">Banner Detayı</li>
					</ul>
					<hr class="soft" />

					<div class="well">
						<div class="container-fluid ">
							<div class="row ">
							<h4>Ürün Detayı</h4>
							<div class="control-group"><label class="control-label" for="lblUrunKodu">Banner Kodu</label>
								<div class="controls">
									<asp:Label ID="lblBannerKodu" runat="server">123</asp:Label>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtUrunAdi">Ürün Adı<sup>*</sup></label><div class="controls ">
									<asp:TextBox ID="txtUrunAdi" runat="server" width="100%"></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtSiralama">Sıralama<sup>*</sup></label><div class="controls">
									<asp:TextBox ID="txtSiralama" runat="server" width="100%"  TextMode="Number"   ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtBannerAciklamasi">Banner Açıklaması</label><div class="controls">
									<asp:TextBox ID="txtBannerAciklamasi" runat="server"  width="100%" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtLink">Link</label><div class="controls">
									<asp:TextBox ID="txtLink" runat="server" width="100%" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group "><label class="control-label" for="txtResimURL">Resim URL</label><div class="controls">
									<asp:TextBox ID="txtResimURL" runat="server" width="100%" ></asp:TextBox>
								</div>
							</div>

							<div class="control-group "><label class="control-label" for="txtAlt">Resim Açıklamasi</label><div class="controls">
									<asp:TextBox ID="txtAlt" runat="server"  width="100%" />
								</div>
							</div>

							<div class="control-group "><label class="control-label" for="txtAlt">Ürün Detayı</label><div class="controls">
									<asp:Label ID="lblDetay2" runat="server"  width="100%" />
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

