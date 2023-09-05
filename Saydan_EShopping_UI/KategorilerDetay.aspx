<%@ Page Title="" Language="VB" MasterPageFile="~/Master2.master" AutoEventWireup="false" CodeFile="KategorilerDetay.aspx.vb" Inherits="KategorilerDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<div id="mainBody">
		<div class="container">
			<div class="row">
				<div class="span12">


					<ul class="breadcrumb">
						<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
						<li><a href="UyeIslemleri.aspx">Üye İşlemleri</a> <span class="divider">/</span></li>
						<li><a href="KategorilerListe.aspx">Kategori Listesi</a> <span class="divider">/</span></li>
						<li class="active">Kategori Detayı</li>
					</ul>
					<hr class="soft" />

					<div class="well">
						<div class="container-fluid ">
							<h4>Kategori Bilgileri</h4>
							<div class="control-group"><label class="control-label" for="lblKategoriKodu">Kategori Kodu</label>
								<div class="controls">
									<asp:Label ID="lblKategoriKodu" runat="server">123</asp:Label>
								</div>
							</div>
							<div class="control-group"><label class="control-label" for="txtKategoriAdi">Kategori Adı<sup>*</sup></label><div class="controls">
									<asp:TextBox ID="txtKategoriAdi" runat="server" Width="100%"></asp:TextBox>
								</div>
							</div>
							<div class="control-group"><label class="control-label" for="drpEbeveyn">Ebeveyn Kategori<sup>*</sup></label><div class="controls">
									<asp:DropDownList ID="drpEbeveyn" runat="server" Width="100%" ></asp:DropDownList>
								</div>
							</div>
							<div class="control-group"><label class="control-label" for="txtKeywords">Keywords</label><div class="controls">
									<asp:TextBox ID="txtKeywords" runat="server"  TextMode="MultiLine" Width="100%" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group"><label class="control-label" for="txtDescription">Description</label><div class="controls">
									<asp:TextBox ID="txtDescription" runat="server" Width="100%" TextMode="MultiLine"  />
								</div>
							</div>
							<div class="control-group"><label class="control-label" for="txtDetay">Kategori Detay Bilgisi</label><div class="controls">
									<asp:TextBox ID="txtDetay" runat="server" Width="100%" TextMode="MultiLine" ></asp:TextBox>
								</div>
							</div>
							<div class="control-group"><label class="control-label" for="txtSiralama">Sıralama</label><div class="controls">
									<asp:TextBox ID="txtSiralama" runat="server" Width="100%" TextMode="Number" >1</asp:TextBox>
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
									<asp:LinkButton ID="btnSil" runat="server" CssClass="btn btn-large pull-left btn-danger">Sil</asp:LinkButton>
									<asp:LinkButton ID="btnRegister" runat="server" CssClass="btn btn-large pull-right btn-default">Kaydet</asp:LinkButton>
								</div>
							</div>
							<p><sup>*</sup>Girilmesi Zorunlu Alanlar</p>
						</div>
					</div>

<span id="spnUrunler" runat="server" >
			<hr class="soft"/>
			<h3>"<asp:Label id="lblKategoriAdi" runat="server" > </asp:Label>"&nbsp; Kategorisindeki Ürünler : </h3>
			<table class="table table-bordered" id="tblUrunler" runat="server"></table>


</span>

				</div>
			</div>
		</div>
	</div>







</asp:Content>

