<%@ Page Title="" Language="VB" MasterPageFile="~/Master2.master" AutoEventWireup="false" CodeFile="index.aspx.vb" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<meta name="keywords" content="Mersin, mersin, borsa, borsasi, mersin borsası, borsasi, otantik, ürün, çukurova, bölgesel, yöresel, yoresel, e-ticaret, ">
	<meta name="description" content="Mersin Borsası, Mersin ve Çukurova Bölgesine özgü değerlerin internet kullanıcılarıyla buluşturulduğu çok amaçlı bir e-ticaret sitesidir. Burada Mersin'e hizmet veren markaları ve ürünlerini bulabilir, tek tıkla sipariş verebilirsiniz." />
	<link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<div id="carouselBlk">
	<div id="myCarousel" class="carousel slide">
		<div class="carousel-inner" id="DivBanner" runat="server" >
		  <div class="item active">
		  <div class="container">
			<a href="register.aspx"><img height="700" src="themes/images/carousel/1.png" alt="special offers"/></a>
			<div class="carousel-caption">
				  <h4>Second Thumbnail label</h4>
				  <p>Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
				</div>
		  </div>
		  </div>
		  <div class="item">
		  <div class="container">
			<a href="product_details.aspx?kod=7943"><img height="700" src="themes/images/carousel/sanal-gerceklik-14.jpg" alt=""/></a>
				<div class="carousel-caption">
				  <h4>Second Thumbnail label</h4>
				  <p>Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
				</div>
		  </div>
		  </div>
		  <div class="item">
		  <div class="container">
			<a href="register.aspx"><img  height="700" src="themes/images/carousel/3.png" alt=""/></a>
			<div class="carousel-caption">
				  <h4>Second Thumbnail label</h4>
				  <p>Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
				</div>
			
		  </div>
		  </div>
		   <div class="item">
		   <div class="container">
			<a href="register.aspx"><img  height="700" src="themes/images/carousel/4.png" alt=""/></a>
			<div class="carousel-caption">
				  <h4>Second Thumbnail label</h4>
				  <p>Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
				</div>
		   
		  </div>
		  </div>
		   <div class="item">
		   <div class="container">
			<a href="register.aspx"><img  height="700" src="themes/images/carousel/5.png" alt=""/></a>
			<div class="carousel-caption">
				  <h4>Second Thumbnail label</h4>
				  <p>Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
			</div>
		  </div>
		  </div>
		</div>
		<a class="left carousel-control" href="#myCarousel" data-slide="prev">&lsaquo;</a>
		<a class="right carousel-control" href="#myCarousel" data-slide="next">&rsaquo;</a>
	  </div> 
</div>
<div id="mainBody">
	<div class="container">
	<div class="row">
<!-- Sidebar ================================================== -->
	<div id="sidebar" class="span3 hidden-phone ">
		<div class="well well-small"><a id="myCart" href="product_summary.aspx">
			<img src="themes/images/ico-cart.png" alt="cart"> <asp:Label ID="lblUrunKalemAdedi2" runat="server" >300</asp:Label> ürün<span class="badge badge-warning pull-right"><asp:Label ID="lblSepettekiUrunTutari2" runat="server">155,25 TL</asp:Label></span></a>
		</div>

<%--		<div class="well well-small"><a id="myCart" href="product_summary.aspx"><img src="themes/images/ico-cart.png" alt="cart">3 Items in your cart  <span class="badge badge-warning pull-right">$155.00</span></a></div>--%>
		<span id="spnSideBar" runat="server"></span>
<%--		<ul id="sideManu" class="nav nav-tabs nav-stacked">
			<li class="subMenu open"><a> ELEKTRONİK [230]</a>
				<ul>
				<li><a class="active" href="products.aspx"><i class="icon-chevron-right"></i>Cameras (100) </a></li>
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Computers, Tablets & laptop (30)</a></li>
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Mobile Phone (80)</a></li>
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Sound & Vision (15)</a></li>
				</ul>
			</li>
			<li class="subMenu"><a> MODA [840] </a>
			<ul style="display:none">
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Women's Clothing (45)</a></li>
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Women's Shoes (8)</a></li>												
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Women's Hand Bags (5)</a></li>	
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Men's Clothings  (45)</a></li>
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Men's Shoes (6)</a></li>												
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Kids Clothing (5)</a></li>												
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Kids Shoes (3)</a></li>												
			</ul>
			</li>
			<li class="subMenu"><a>SUPERMARKET [1000]</a>
				<ul style="display:none">
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Angoves  (35)</a></li>
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Bouchard Aine & Fils (8)</a></li>												
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>French Rabbit (5)</a></li>	
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Louis Bernard  (45)</a></li>
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>BIB Wine (Bag in Box) (8)</a></li>												
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Other Liquors & Wine (5)</a></li>												
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Garden (3)</a></li>												
				<li><a href="products.aspx"><i class="icon-chevron-right"></i>Khao Shong (11)</a></li>												
			</ul>
			</li>
			<li><a href="products.aspx">KOZMATIK [18]</a></li>
			<li><a href="products.aspx">SPORT & OUTDOOR [58]</a></li>
			<li><a href="products.aspx">KITAP & DERGI [14]</a></li>
		</ul>--%>

		<br/>
<%--		  <div class="thumbnail">
			<img src="themes/images/products/panasonic.jpg" alt="Bootshop panasonoc New camera"/>
			<div class="caption">
			  <h5>Panasonic</h5>
				<h4 style="text-align:center"><a class="btn" href="product_details.aspx"> <i class="icon-zoom-in"></i></a> <a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a> <a class="btn btn-primary" href="#">$222.00</a></h4>
			</div>
		  </div><br/>
			<div class="thumbnail">
				<img src="themes/images/products/kindle.png" title="Bootshop New Kindel" alt="Bootshop Kindel">
				<div class="caption">
				  <h5>Kindle</h5>
				    <h4 style="text-align:center"><a class="btn" href="product_details.aspx"> <i class="icon-zoom-in"></i></a> <a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a> <a class="btn btn-primary" href="#">$222.00</a></h4>
				</div>
			  </div><br/>--%>
<%--			<div class="thumbnail">
				<img src="themes/images/payment_methods.png" title="Ödeme Seçenekleri" alt="Ödeme Seçenekleri">
				<div class="caption">
				  <h5>Ödeme Seçenekleri</h5>
				</div>
			  </div>--%>
	</div>
<!-- Sidebar end=============================================== -->
		<div class="span9">		
			<div class="well well-small">
			<h4>İndirimdeki Ürünler <small class="pull-right"><asp:Label id="lblFeatCnt" runat="server">200+</asp:Label> adet ürün</small></h4>
			<div class="row-fluid">
			<span id="spnFeat" runat="server">
			<div id="featured" class="carousel slide">
			<div class="carousel-inner">
			  <div class="item active">
			  <ul class="thumbnails">
				<li class="span3">
				  <div class="thumbnail">
				  <i class="tag"></i>
					<a href="product_details.aspx"><img src="themes/images/products/b1.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					  <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
				  <i class="tag"></i>
					<a href="product_details.aspx"><img src="themes/images/products/b2.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					  <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
				  <i class="tag"></i>
					<a href="product_details.aspx"><img src="themes/images/products/b3.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					   <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
				  <i class="tag"></i>
					<a href="product_details.aspx"><img src="themes/images/products/b4.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					   <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
			  </ul>
			  </div>
			   <div class="item">
			  <ul class="thumbnails">
				<li class="span3">
				  <div class="thumbnail">
				  <i class="tag"></i>
					<a href="product_details.aspx"><img src="themes/images/products/5.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					  <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
				  <i class="tag"></i>
					<a href="product_details.aspx"><img src="themes/images/products/6.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					  <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
					<a href="product_details.aspx"><img src="themes/images/products/7.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					   <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
					<a href="product_details.aspx"><img src="themes/images/products/8.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					   <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
			  </ul>
			  </div>
			   <div class="item">
			  <ul class="thumbnails">
				<li class="span3">
				  <div class="thumbnail">
					<a href="product_details.aspx"><img src="themes/images/products/9.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					  <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
					<a href="product_details.aspx"><img src="themes/images/products/10.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					  <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
					<a href="product_details.aspx"><img src="themes/images/products/11.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					   <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
					<a href="product_details.aspx"><img src="themes/images/products/1.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					   <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
			  </ul>
			  </div>
			   <div class="item">
			  <ul class="thumbnails">
				<li class="span3">
				  <div class="thumbnail">
					<a href="product_details.aspx"><img src="themes/images/products/2.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					  <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
					<a href="product_details.aspx"><img src="themes/images/products/3.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					  <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
					<a href="product_details.aspx"><img src="themes/images/products/4.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					   <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
				<li class="span3">
				  <div class="thumbnail">
					<a href="product_details.aspx"><img src="themes/images/products/5.jpg" alt=""></a>
					<div class="caption">
					  <h5>Product name</h5>
					   <h4><a class="btn" href="product_details.aspx">VIEW</a> <span class="pull-right">$222.00</span></h4>
					</div>
				  </div>
				</li>
			  </ul>
			  </div>
			  </div>
			  <a class="left carousel-control" href="#featured" data-slide="prev">‹</a>
			  <a class="right carousel-control" href="#featured" data-slide="next">›</a>
			  </div>
				  </span>
			  </div>
		</div>
		<h4>Özel Ürünler</h4>
			<span id="spnSpec" runat="server">
				  <ul class="thumbnails">
					<li class="span3">
					  <div class="thumbnail">
						<a  href="product_details.aspx"><img src="themes/images/products/6.jpg" alt=""/></a>
						<div class="caption">
						  <h5>Product name</h5>
						  <p> 
							Lorem Ipsum is simply dummy text. 
						  </p>
					 
						  <h4 style="text-align:center"><a class="btn" href="product_details.aspx"> <i class="icon-zoom-in"></i></a> <a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a> <a class="btn btn-primary" href="#">$222.00</a></h4>
						</div>
					  </div>
					</li>
					<li class="span3">
					  <div class="thumbnail">
						<a  href="product_details.aspx"><img src="themes/images/products/7.jpg" alt=""/></a>
						<div class="caption">
						  <h5>Product name</h5>
						  <p> 
							Lorem Ipsum is simply dummy text. 
						  </p>
						 <h4 style="text-align:center"><a class="btn" href="product_details.aspx"> <i class="icon-zoom-in"></i></a> <a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a> <a class="btn btn-primary" href="#">$222.00</a></h4>
						</div>
					  </div>
					</li>
					<li class="span3">
					  <div class="thumbnail">
						<a  href="product_details.aspx"><img src="themes/images/products/8.jpg" alt=""/></a>
						<div class="caption">
						  <h5>Product name</h5>
						  <p> 
							Lorem Ipsum is simply dummy text. 
						  </p>
						   <h4 style="text-align:center"><a class="btn" href="product_details.aspx"> <i class="icon-zoom-in"></i></a> <a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a> <a class="btn btn-primary" href="#">$222.00</a></h4>
						</div>
					  </div>
					</li>
					<li class="span3">
					  <div class="thumbnail">
						<a  href="product_details.aspx"><img src="themes/images/products/9.jpg" alt=""/></a>
						<div class="caption">
						  <h5>Product name</h5>
						  <p> 
							Lorem Ipsum is simply dummy text. 
						  </p>
						  <h4 style="text-align:center"><a class="btn" href="product_details.aspx"> <i class="icon-zoom-in"></i></a> <a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a> <a class="btn btn-primary" href="#">$222.00</a></h4>
						</div>
					  </div>
					</li>
					<li class="span3">
					  <div class="thumbnail">
						<a  href="product_details.aspx"><img src="themes/images/products/10.jpg" alt=""/></a>
						<div class="caption">
						  <h5>Product name</h5>
						  <p> 
							Lorem Ipsum is simply dummy text. 
						  </p>
						  <h4 style="text-align:center"><a class="btn" href="product_details.aspx"> <i class="icon-zoom-in"></i></a> <a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a> <a class="btn btn-primary" href="#">$222.00</a></h4>
						</div>
					  </div>
					</li>
					<li class="span3">
					  <div class="thumbnail">
						<a  href="product_details.aspx"><img src="themes/images/products/11.jpg" alt=""/></a>
						<div class="caption">
						  <h5>Product name</h5>
						  <p> 
							Lorem Ipsum is simply dummy text. 
						  </p>
						   <h4 style="text-align:center"><a class="btn" href="product_details.aspx"> <i class="icon-zoom-in"></i></a> <a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a> <a class="btn btn-primary" href="#">$222.00</a></h4>
						</div>
					  </div>
					</li>
				  </ul>	

			</span>

		</div>
		</div>
	</div>
</div>
<img src="themes/images/creditcardlogos.png" />
</asp:Content>

