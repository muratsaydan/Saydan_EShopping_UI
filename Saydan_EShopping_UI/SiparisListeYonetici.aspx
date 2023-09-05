<%@ Page Title="" Language="VB" MasterPageFile="~/Master2.master" AutoEventWireup="false" CodeFile="SiparisListeYonetici.aspx.vb" Inherits="SiparisListe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<div id="mainBody">
		<div class="container">
			<div class="row">
				<div class="span12">


					<ul class="breadcrumb">
						<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
						<li><a href="UyeIslemleri.aspx">Üye İşlemleri</a> <span class="divider">/</span></li>
						<li class="active">İşlemdeki Siparişler</li>
					</ul>
					<hr class="soft" />
					<h3>İade Edilen Siparişler : </h3>
					<table class="table table-bordered" id="tblIadeEdilenSiparisler" runat="server"></table>
					<hr class="soft" />
					<h3>Ödemesi Yapılmış Siparişler : </h3>
					<table class="table table-bordered" id="tblOdemesiYapilanlar" runat="server"></table>
					<hr class="soft" />
					<h3>Tedarikçiye Yönlendirilmiş Siparişler : </h3>
					<table class="table table-bordered" id="tblTedarikciyeYonlendirilenler" runat="server"></table>
					<hr class="soft" />
					<h3>Kargoya Teslim Edilmişler : </h3>
					<table class="table table-bordered" id="tblKargoyaVerilenler" runat="server"></table>


				</div>
			</div>
		</div>
	</div>







</asp:Content>

