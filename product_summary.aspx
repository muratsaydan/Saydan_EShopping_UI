<%@ Page Title="" Language="VB" MasterPageFile="~/Master1.master" AutoEventWireup="false" CodeFile="product_summary.aspx.vb" Inherits="product_summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<div class="span9">
    <ul class="breadcrumb">
		<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
		<li class="active"> ALIŞVERİŞ SEPETİ</li>
    </ul>
	<h3> ALIŞVERİŞ SEPETİ [ <small><asp:Label id="lblAdet" runat="server" >3</asp:Label>  Ürün &nbsp;</small>]<a href="products.aspx" class="btn btn-large pull-right"><i class="icon-arrow-left"></i> Alışverişe Devam Et</a></h3>	
	<hr class="soft"/>

			<table class="table table-bordered" id="tbl1" runat="server">
				<%--<thead><tr><th>#</th><th>Ürün</th><th>Açıklama</th><th>Adet</th><th>Birim Fiyat</th><th>Toplam</th></tr></thead><tbody>
                <tr>
				 <td>1</td>
                  <td> <img width="60" src="themes/images/products/4.jpg" alt=""/></td>
                  <td>MASSA AST<br/>Color : black, Material : metal</td>
				  <td>
					<div class="input-append">
						<input class="span1" style="max-width:34px" placeholder="1" id="appendedInputButtons" size="16" type="text">
						<button class="btn" type="button"><i class="icon-minus"></i></button>
						<button class="btn" type="button"><i class="icon-plus"></i></button>
						<button class="btn btn-danger" type="button"><i class="icon-remove icon-white"></i></button>				
					</div>
				  </td>
                  <td>$120.00</td>
                  <td>$110.00</td>
                </tr>
				
                <tr><td colspan="5" style="text-align:right">Toplam Tutar :	</td><td> $228.00</td></tr>
                 <tr>
                  <td colspan="5" style="text-align:right">Kargo Ücreti :	</td>
                  <td> $31.00</td>
                </tr>
				 <tr>
                  <td colspan="5" style="text-align:right"><strong>Toplam :</strong></td>
                  <td class="label label-important" style="display:block"> <strong> $155.00 </strong></td>
                </tr>
				</tbody>--%>
			</table>


			<%--            <table class="table table-bordered">
			<tbody>
				 <tr>
                  <td> 
				<form class="form-horizontal">
				<div class="control-group">
				<label class="control-label"><strong> VOUCHERS CODE: </strong> </label>
				<div class="controls">
				<input type="text" class="input-medium" placeholder="CODE">
				<button type="submit" class="btn"> ADD </button>
				</div>
				</div>
				</form>
				</td>
                </tr>
				
			</tbody>
			</table>--%>
			
<%--			<table class="table table-bordered">
			 <tr><th>ESTIMATE YOUR SHIPPING </th></tr>
			 <tr> 
			 <td>
				<form class="form-horizontal">
				  <div class="control-group">
					<label class="control-label" for="inputCountry">Country </label>
					<div class="controls">
					  <input type="text" id="inputCountry" placeholder="Country">
					</div>
				  </div>
				  <div class="control-group">
					<label class="control-label" for="inputPost">Post Code/ Zipcode </label>
					<div class="controls">
					  <input type="text" id="inputPost" placeholder="Postcode">
					</div>
				  </div>
				  <div class="control-group">
					<div class="controls">
					  <button type="submit" class="btn">ESTIMATE </button>
					</div>
				  </div>
				</form>				  
			  </td>
			  </tr>
            </table>		--%>
	<a href="products.aspx" class="btn btn-large"><i class="icon-arrow-left"></i> Alışverişe Devam Et</a>
	<a href="SiparisDetay.aspx" class="btn btn-large pull-right">İleri <i class="icon-arrow-right"></i></asp:LinkButton>
	
</div>

</asp:Content>

