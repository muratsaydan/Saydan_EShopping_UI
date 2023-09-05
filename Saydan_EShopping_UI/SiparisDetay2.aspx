<%@ Page Title="" Language="VB" MasterPageFile="~/Master1.master" AutoEventWireup="false" CodeFile="SiparisDetay2.aspx.vb" Inherits="SiparisDetay2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="span9">
		<ul class="breadcrumb">
			<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
			<li class="active">Teslimat Bilgileri</li>
		</ul>
		<hr class="soft" />

		<table class="table table-bordered" id="tbl1" runat="server">
		</table>

		<div class="well">
			<div class="container-fluid ">
				&nbsp;
				
				<h4>Teslimat Bilgileri</h4>
				<br />
				<div class="control-group">
					<label class="control-label" for="txtTeslimatAdresi" style="font-weight: bold;">Teslimat Adresi</label>
					<div class="controls">
						<asp:Label ID="txtTeslimatAdresi" runat="server" placeholder=" Teslimat Adresi" Width="100%" TextMode="MultiLine"></asp:Label>
					</div>
				</div>

				<div class="control-group  ">
					<label class="control-label" for="txtTeslimatSehri" style="font-weight: bold;">Teslimat Şehri</label>
					<div class="controls">
						<asp:Label ID="txtTeslimatSehri" runat="server" placeholder=" Teslimat Şehri" Width="100%"></asp:Label>
					</div>
				</div>


				<h4>Fatura Bilgileri</h4>
				<br />
				<div class="control-group  ">
					<label class="control-label" for="txtSirket" style="font-weight: bold;">Firma Ünvanı</label>
					<div class="controls">
						<asp:Label ID="txtSirket" runat="server" placeholder="Şirket" Width="100%"></asp:Label>
					</div>
				</div>

				<div class="control-group  ">
					<label class="control-label" for="txtFaturaAdresi" style="font-weight: bold;">Fatura Adresi</label>
					<div class="controls">

						<asp:Label runat="server" ID="txtFaturaAdresi" placeholder=" Fatura Adresi" Width="100%" TextMode="MultiLine" />
					</div>
				</div>

				<div class="control-group ">
					<label class="control-label" for="txtVergiDairesi" style="font-weight: bold;">Vergi Dairesi</label>
					<div class="controls">
						<asp:Label ID="txtVergiDairesi" runat="server" placeholder=" Vergi Dairesi" Width="100%" />
					</div>
				</div>

				<div class="control-group  ">
					<label class="control-label" for="txtVergiNo" style="font-weight: bold;">Vergi Numarası</label>
					<div class="controls">
						<asp:Label ID="txtVergiNo" runat="server" placeholder=" Vergi Numarası" Width="100%" />
					</div>
				</div>

				<div class="control-group">
					<label class="control-label" for="txtEkBilgiler" style="font-weight: bold;">Ek Bilgiler</label>
					<div class="controls">
						<asp:Label runat="server" TextMode="MultiLine" Width="100%" ID="txtEkBilgiler" placeholder="Ek Bilgiler"></asp:Label>
					</div>
				</div>

			</div>
		</div>

		<div class="" style="background-color:tomato; padding:10px;" id="divyonetici" runat="server" >

			<h4>Yönetici Paneli</h4>
		<div class="control-group  " >
			<label class="control-label" for="txtTedarikciSiparisKodu">Tedarikçi Sipariş Kodu<sup>*</sup></label>
			<div class="controls">
				<asp:TextBox ID="txtTedarikciSiparisKodu" runat="server" placeholder=" Tedarikçi Sipariş Kodu" width="90%" ></asp:TextBox>
			</div>
		</div>

		<div class="control-group  " >
			<label class="control-label" for="txtTeslimatSehri">Kargo Firması<sup>*</sup></label>
			<div class="controls">
				<asp:TextBox ID="txtKargoFirmasi" runat="server" placeholder=" Kargo Firması" width="90%" ></asp:TextBox>
			</div>
		</div>
		<div class="control-group  " >
			<label class="control-label" for="txtTeslimatSehri">Kargo Referans No<sup>*</sup></label>
			<div class="controls">
				<asp:TextBox ID="txtKargoNo" runat="server" placeholder="Kargo Referans No" width="90%" ></asp:TextBox>
			</div>
		</div>


			<div class="checkbox"> 
				<asp:CheckBoxList ID="chkSecim" runat="server" >
					<asp:ListItem >Tedarikçiye Sipariş Verildi</asp:ListItem>
					<asp:ListItem >Kargoya Verildi</asp:ListItem>
					<asp:ListItem >Müşteriye Teslim Edildi</asp:ListItem>
				</asp:CheckBoxList>
			</div>


			
			<asp:Button ID="btnKaydet" runat="server" Text="Kaydet" cssclass="btn btn-info btn-large  pull-right " />
			<br /><br />
			&nbsp;
		</div>
		<br />

		<a href="SiparisListe.aspx" class="btn btn-large"><i class="icon-arrow-left"></i>Geri Dön</a>

	</div>

</asp:Content>

