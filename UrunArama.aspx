﻿<%@ Page Title="" Language="VB" EnableEventValidation="false"  MasterPageFile="~/Master1.master" AutoEventWireup="false" CodeFile="UrunArama.aspx.vb" Inherits="UrunArama" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
						<div class="span9">

						<ul class="breadcrumb">
							<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
							<li class="active">Ürünler</li>
						</ul>
						<h3 ><asp:Label ID="lblUrunKategorisi" runat="server">Products Name </asp:Label><small class="pull-right"> Bu kategoride <asp:Label id="lblUrunAdedi" runat="server">40</asp:Label> ürün bulunmaktadır.</small></h3>
						<hr class="soft" />
						<p id="spnUrunKategorisiAciklama" runat="server">
						</p>
						<hr class="soft" />
<%--						<form class="form-horizontal span6">
							<div class="control-group">
								<label class="control-label alignL">Sort By </label>
								<select>
									<option>Priduct name A - Z</option>
									<option>Priduct name Z - A</option>
									<option>Priduct Stoke</option>
									<option>Price Lowest first</option>
								</select>
							</div>
						</form>--%>

						<div id="myTab" class="pull-right">
							<a href="#listView" data-toggle="tab"><span class="btn btn-large"><i class="icon-list"></i></span></a>
							<a href="#blockView" data-toggle="tab"><span class="btn btn-large btn-primary"><i class="icon-th-large"></i></span></a>
						</div>
						<br class="clr" />
						<div class="tab-content">
							<div class="tab-pane" id="listView" >

								<span id="spnListe1" runat="server">
								<div class="row">
									<div class="span2">
										<img src="themes/images/products/3.jpg" alt="" />
									</div>
									<div class="span4">
										<h3>New | Available</h3>
										<hr class="soft" />
										<h5>Product Name </h5>
										<p>
<%--											Nowadays the lingerie industry is one of the most successful business spheres.We always stay in touch with the latest fashion tendencies - 
				that is why our goods are so popular..--%>
										</p>
										<a class="btn btn-small pull-right" href="product_details.aspx">View Details</a>
										<br class="clr" />
									</div>
									<div class="span3 alignR">
										<form class="form-horizontal qtyFrm">
											<h3>$140.00</h3>
											<label class="checkbox">
												<input type="checkbox">
												Adds product to compair
											</label>
											<br />

											<a href="product_details.aspx" class="btn btn-large btn-primary">Sepete Ekle <i class=" icon-shopping-cart"></i></a>
											<a href="product_details.aspx" class="btn btn-large"><i class="icon-zoom-in"></i></a>

										</form>
									</div>
								</div>
								<hr class="soft" />
								<div class="row">
									<div class="span2">
										<img src="themes/images/products/1.jpg" alt="" />
									</div>
									<div class="span4">
										<h3>New | Available</h3>
										<hr class="soft" />
										<h5>Product Name </h5>
										<p>
											Nowadays the lingerie industry is one of the most successful business spheres.We always stay in touch with the latest fashion tendencies - 
				that is why our goods are so popular..
										</p>
										<a class="btn btn-small pull-right" href="product_details.aspx">View Details</a>
										<br class="clr" />
									</div>
									<div class="span3 alignR">
										<form class="form-horizontal qtyFrm">
											<h3>$140.00</h3>
											<label class="checkbox">
												<input type="checkbox">
												Adds product to compair
											</label>
											<br />

											<a href="product_details.aspx" class="btn btn-large btn-primary">Sepete Ekle <i class=" icon-shopping-cart"></i></a>
											<a href="product_details.aspx" class="btn btn-large"><i class="icon-zoom-in"></i></a>

										</form>
									</div>
								</div>
								<hr class="soft" />
								<div class="row">
									<div class="span2">
										<img src="themes/images/products/3.jpg" alt="" />
									</div>
									<div class="span4">
										<h3>New | Available</h3>
										<hr class="soft" />
										<h5>Product Name </h5>
										<p>
											Nowadays the lingerie industry is one of the most successful business spheres.We always stay in touch with the latest fashion tendencies - 
				that is why our goods are so popular..
										</p>
										<a class="btn btn-small pull-right" href="product_details.aspx">View Details</a>
										<br class="clr" />
									</div>
									<div class="span3 alignR">
										<form class="form-horizontal qtyFrm">
											<h3>$140.00</h3>
											<label class="checkbox">
												<input type="checkbox">
												Adds product to compair
											</label>
											<br />

											<a href="product_details.aspx" class="btn btn-large btn-primary">Sepete Ekle <i class=" icon-shopping-cart"></i></a>
											<a href="product_details.aspx" class="btn btn-large"><i class="icon-zoom-in"></i></a>

										</form>
									</div>
								</div>
								<hr class="soft" />
								<div class="row">
									<div class="span2">
										<img src="themes/images/products/3.jpg" alt="" />
									</div>
									<div class="span4">
										<h3>New | Available</h3>
										<hr class="soft" />
										<h5>Product Name </h5>
										<p>
											Nowadays the lingerie industry is one of the most successful business spheres.We always stay in touch with the latest fashion tendencies - 
				that is why our goods are so popular..
										</p>
										<a class="btn btn-small pull-right" href="product_details.aspx">View Details</a>
										<br class="clr" />
									</div>
									<div class="span3 alignR">
										<form class="form-horizontal qtyFrm">
											<h3>$140.00</h3>
											<label class="checkbox">
												<input type="checkbox">
												Adds product to compair
											</label>
											<br />

											<a href="product_details.aspx" class="btn btn-large btn-primary">Sepete Ekle <i class=" icon-shopping-cart"></i></a>
											<a href="product_details.aspx" class="btn btn-large"><i class="icon-zoom-in"></i></a>

										</form>
									</div>
								</div>

								<hr class="soft" />
								<div class="row">
									<div class="span2">
										<img src="themes/images/products/3.jpg" alt="" />
									</div>
									<div class="span4">
										<h3>New | Available</h3>
										<hr class="soft" />
										<h5>Product Name </h5>
										<p>
											Nowadays the lingerie industry is one of the most successful business spheres.We always stay in touch with the latest fashion tendencies - 
				that is why our goods are so popular..
										</p>
										<a class="btn btn-small pull-right" href="product_details.aspx">View Details</a>
										<br class="clr" />
									</div>
									<div class="span3 alignR">
										<form class="form-horizontal qtyFrm">
											<h3>$140.00</h3>
											<label class="checkbox">
												<input type="checkbox">
												Adds product to compair
											</label>
											<br />
											<a href="product_details.aspx" class="btn btn-large btn-primary">Sepete Ekle <i class=" icon-shopping-cart"></i></a>
											<a href="product_details.aspx" class="btn btn-large"><i class="icon-zoom-in"></i></a>
										</form>
									</div>
								</div>
								<hr class="soft" />
								<div class="row">
									<div class="span2">
										<img src="themes/images/products/3.jpg" alt="" />
									</div>
									<div class="span4">
										<h3>New | Available</h3>
										<hr class="soft" />
										<h5>Product Name </h5>
										<p>
											Nowadays the lingerie industry is one of the most successful business spheres.We always stay in touch with the latest fashion tendencies - 
				that is why our goods are so popular..
										</p>
										<a class="btn btn-small pull-right" href="product_details.aspx">View Details</a>
										<br class="clr" />
									</div>
									<div class="span3 alignR">
										<form class="form-horizontal qtyFrm">
											<h3>$140.00</h3>
											<label class="checkbox">
												<input type="checkbox">
												Adds product to compair
											</label>
											<br />

											<a href="product_details.aspx" class="btn btn-large btn-primary">Sepete Ekle <i class=" icon-shopping-cart"></i></a>
											<a href="product_details.aspx" class="btn btn-large"><i class="icon-zoom-in"></i></a>

										</form>
									</div>
								</div>
								<hr class="soft" />
								</span>

							</div>

							<div class="tab-pane  active" id="blockView">

								<span id="spnListe2" runat="server">
								<ul class="thumbnails">
									<li class="span3">
										<div class="thumbnail">
											<a href="product_details.aspx">
												<img src="themes/images/products/3.jpg" alt="" /></a>
											<div class="caption">
												<h5>Manicure &amp; Pedicure</h5>
												<p>
													I'm a paragraph. Click here 
												</p>
												<h4 style="text-align: center"><a class="btn" href="product_details.aspx"><i class="icon-zoom-in"></i></a><a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a><a class="btn btn-primary" href="#">&euro;222.00</a></h4>
											</div>
										</div>
									</li>
									<li class="span3">
										<div class="thumbnail">
											<a href="product_details.aspx">
												<img src="themes/images/products/3.jpg" alt="" /></a>
											<div class="caption">
												<h5>Manicure &amp; Pedicure</h5>
												<p>
													I'm a paragraph. Click here 
												</p>
												<h4 style="text-align: center"><a class="btn" href="product_details.aspx"><i class="icon-zoom-in"></i></a><a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a><a class="btn btn-primary" href="#">&euro;222.00</a></h4>
											</div>
										</div>
									</li>
									<li class="span3">
										<div class="thumbnail">
											<a href="product_details.aspx">
												<img src="themes/images/products/3.jpg" alt="" /></a>
											<div class="caption">
												<h5>Manicure &amp; Pedicure</h5>
												<p>
													I'm a paragraph. Click here 
												</p>
												<h4 style="text-align: center"><a class="btn" href="product_details.aspx"><i class="icon-zoom-in"></i></a><a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a><a class="btn btn-primary" href="#">&euro;222.00</a></h4>
											</div>
										</div>
									</li>
									<li class="span3">
										<div class="thumbnail">
											<a href="product_details.aspx">
												<img src="themes/images/products/3.jpg" alt="" /></a>
											<div class="caption">
												<h5>Manicure &amp; Pedicure</h5>
												<p>
													I'm a paragraph. Click here 
												</p>
												<h4 style="text-align: center"><a class="btn" href="product_details.aspx"><i class="icon-zoom-in"></i></a><a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a><a class="btn btn-primary" href="#">&euro;222.00</a></h4>
											</div>
										</div>
									</li>
									<li class="span3">
										<div class="thumbnail">
											<a href="product_details.aspx">
												<img src="themes/images/products/3.jpg" alt="" /></a>
											<div class="caption">
												<h5>Manicure &amp; Pedicure</h5>
												<p>
													I'm a paragraph. Click here 
												</p>
												<h4 style="text-align: center"><a class="btn" href="product_details.aspx"><i class="icon-zoom-in"></i></a><a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a><a class="btn btn-primary" href="#">&euro;222.00</a></h4>
											</div>
										</div>
									</li>
									<li class="span3">
										<div class="thumbnail">
											<a href="product_details.aspx">
												<img src="themes/images/products/3.jpg" alt="" /></a>
											<div class="caption">
												<h5>Manicure &amp; Pedicure</h5>
												<p>
													I'm a paragraph. Click here 
												</p>
												<h4 style="text-align: center"><a class="btn" href="product_details.aspx"><i class="icon-zoom-in"></i></a><a class="btn" href="#">Sepete Ekle <i class="icon-shopping-cart"></i></a><a class="btn btn-primary" href="#">&euro;222.00</a></h4>
											</div>
										</div>
									</li>
								</ul>
								</span>

								<hr class="soft" />
							</div>
						</div>

<%--						<a href="compair.aspx" class="btn btn-large pull-right">Compair Product</a>--%>
						<div class="pagination">
							<ul>
								<li><a href="#">&lsaquo;</a></li>
								<li><a href="#">1</a></li>
								<li><a href="#">2</a></li>
								<li><a href="#">3</a></li>
								<li><a href="#">4</a></li>
								<li><a href="#">...</a></li>
								<li><a href="#">&rsaquo;</a></li>
							</ul>
						</div>
						<br class="clr" />
</div>
</asp:Content>

