<%@ Page Title="" Language="VB" MasterPageFile="~/Master2.master" AutoEventWireup="false" CodeFile="XMLUpdate.aspx.vb" Inherits="SiparisListe" %>

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
						<li class="active">XML Update</li>
					</ul>
					<hr class="soft" />
					<h4>Mersin Borsasına Yükle</h4>
						    <span id="spnbutonlar" runat="server" ></span><br /><br />
					<span style="display :none;">
					<h4>N11.Com'a yüklemek Üzere Hazırla</h4>
						    <span id="spnN11eHazirla" runat="server" ></span><br /><br />
						    <span id="spn1" runat="server" ></span>
					</span>

				</div>
			</div>
		</div>
	</div>







</asp:Content>

