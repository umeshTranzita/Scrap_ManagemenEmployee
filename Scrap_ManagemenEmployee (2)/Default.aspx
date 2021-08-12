<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dentist - Products</title>
    <meta name="viewport" content="width=device-width, initial-scale=1 maximum-scale=1" />
    <link rel="icon" href="favicon.ico" type="image/x-icon" />
    <!-- CSS  -->
    <link rel="stylesheet" href="lib/font-awesome/web-fonts-with-css/css/fontawesome-all.css" />
    <link rel="stylesheet" href="css/materialize.min.css" />
    <link rel="stylesheet" href="css/normalize.css" />
    <link rel="stylesheet" href="css/style.css" />
    <!-- materialize icon -->
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
    <!-- Owl carousel -->
    <link rel="stylesheet" href="lib/owlcarousel/assets/owl.carousel.min.css" />
    <link rel="stylesheet" href="lib/owlcarousel/assets/owl.theme.default.min.css" />
    <!-- Slick CSS -->
    <link rel="stylesheet" type="text/css" href="lib/slick/slick/slick.css" />
    <link rel="stylesheet" type="text/css" href="lib/slick/slick/slick-theme.css" />
    <!-- Magnific Popup core CSS file -->
    
    
    <style>      

        .side-nav {
            -webkit-transform: translateX(-100%);
            transform: translateX(-100%);
        }
        header {
            border-bottom: 2px solid #179ce4;
        }


        @media screen and (min-width: 0px) and (max-width: 768px) {
            

            #page-content {
                margin-left: 0px;
            }

            nav {
                height: 56px;
            }

            body nav {
                height: 56px !important;
                border-top: none;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- BEGIN PRELOADING -->
        <div class="preloading">
            <div class="wrap-preload">
                <div class="cssload-loader"></div>
            </div>
        </div>

        <header id="header">
            <div class="nav-wrapper container">
                <div class="header-menu-button">
                    <a href="#" data-activates="nav-mobile-category" class="button-collapse" id="button-collapse-category">
                        <div class="cst-btn-menu">
                            <i class="fas fa-align-left"></i>
                        </div>
                    </a>
                </div>
                <div class="header-logo">
                    <a href="#" class="nav-logo">
                        <img src="img/logo_icon.png" /></a>
                </div>
                <div class="header-icon-menu">
                    <a href="#" data-activates="nav-mobile-account" class="button-collapse" id="button-collapse-account"><i class="fas fa-search"></i></a>
                </div>
            </div>
        </header>

        <nav>

            <!-- LEFT SIDENAV-->
            <ul id="nav-mobile-category" class="side-nav">
                <li class="profile">
                    <div class="li-profile-info">
                        <img src="img/profile4.jpg" alt="profile">
                        <h2>John Doe</h2>
                        <div class="emailprofile">email@email.com</div>
                    </div>
                    <div class="bg-profile-li">
                        <img alt="photo" src="img/bg-profile.jpg">
                    </div>
                </li>
                <li>
                    <a class="waves-effect waves-blue" href="#"><i class="fas fa-home"></i>Home</a>
                </li>
                <li>
                    <a href="our-doctors.html"><i class="fas fa-user-md"></i>Our Doctors</a>
                </li>
                <li>
                    <a class="waves-effect waves-blue" href="product-list.html"><i class="fas fa-shopping-bag"></i>Product List</a>
                </li>
                <li>
                    <a class="waves-effect waves-blue" href="signup.html"><i class="fas fa-registered"></i>Doctor Sign Up</a>
                </li>
                <li>
                    <a href="Login.aspx"><i class="fas fa-sign-out-alt"></i>Sign Out</a>
                </li>
            </ul>
            <!-- END LEFT SIDENAV-->


        </nav>
        <!-- END SIDENAV-->
        <!-- MAIN SLIDER -->
        <div class="main-slider" data-indicators="true">
            <div class="carousel carousel-slider " data-indicators="true">
                <a class="carousel-item">
                    <img src="img/slide.jpg" alt="slider"></a>
                <a class="carousel-item">
                    <img src="img/slide2.jpg" alt="slider"></a>
            </div>
        </div>
        <!-- END MAIN SLIDER -->
        <!-- CATEGORY -->
        <div class="section home-category">
            <div class="container">
                
                <div class="row icon-service">
                    <div class="col s4 m4 l2">
                        <a class="icon-content">
                            <div class="content fadetransition">
                                <div class="in-content">
                                    <div class="in-in-content">
                                        <img src="img/care.png" alt="category">
                                        <h5>Asurance</h5>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col s4 m4 l2">
                        <a class="icon-content">
                            <div class="content fadetransition">
                                <div class="in-content">
                                    <div class="in-in-content">
                                        <img src="img/hospital.png" alt="category">
                                        <h5>Find<br>
                                            Hospital</h5>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col s4 m4 l2">
                        <a class="icon-content">
                            <div class="content fadetransition">
                                <div class="in-content">
                                    <div class="in-in-content">
                                        <img src="img/doctor.png" alt="category">
                                        <h5>Find<br>
                                            Doctor</h5>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col s4 m4 l2">
                        <a class="icon-content">
                            <div class="content fadetransition">
                                <div class="in-content">
                                    <div class="in-in-content">
                                        <img src="img/store.png" alt="category">
                                        <h5>Find<br>
                                            Medicine</h5>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col s4 m4 l2">
                        <a class="icon-content">
                            <div class="content fadetransition">
                                <div class="in-content">
                                    <div class="in-in-content">
                                        <img src="img/medical-report.png" alt="category">
                                        <h5>Medical<br>
                                            Records</h5>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col s4 m4 l2">
                        <a class="icon-content">
                            <div class="content fadetransition">
                                <div class="in-content">
                                    <div class="in-in-content">
                                        <img src="img/stethoscope.png" alt="category">
                                        <h5>Health<br>
                                            Information</h5>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <!-- END CATEGORY -->

        <!-- NEWS -->
        <div class="section list-news">
            <div class="container">
                <div class="row row-title">
                    <div class="col s12">
                        <div class="section-title">
                            <span class="theme-secondary-color">HEALTH</span> INFORMATION
                        </div>
                    </div>
                </div>
                <div class="row row-list-news">
                    <div class="col s12">

                        <!-- News item-->
                        <div class="news-item">
                            <div class="news-tem-image">
                                <img src="img/news1.jpg">
                            </div>
                            <div class="news-item-info">
                                <div class="list-news-title">
                                    Benefits of flossing
                                </div>
                                Daily flossing is necessary for removing plaque and food particles that your toothbrush cannot reach.
            <a href="news-page.html" class="readmore">Read More</a>
                            </div>
                        </div>
                        <!-- End news item-->
                        <!-- News item-->
                        <div class="news-item">
                            <div class="news-tem-image">
                                <img src="img/news2.jpg">
                            </div>
                            <div class="news-item-info">
                                <div class="list-news-title">
                                    Causes and treatments for bumps on the gums
                                </div>
                                A bump on the gums is a common occurrence, and most bumps are relatively harmless.
            <a href="news-page.html" class="readmore">Read More</a>
                            </div>
                        </div>
                        <!-- End news item-->
                        <!-- News item-->
                        <div class="news-item">
                            <div class="news-tem-image">
                                <img src="img/news3.jpg">
                            </div>
                            <div class="news-item-info">
                                <div class="list-news-title">
                                    Tips for Safe Drinking Tea
                                </div>
                                Excepteur sint occaecat cupidatat non proident, sunt in culpa qui...
            <a href="news-page.html" class="readmore">Read More</a>
                            </div>
                        </div>
                        <!-- End news item-->
                        <div class="more-news-list">
                            <a class="more-btn" href="news-list.html">See More News &gt;</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <!-- END NEWS -->
        <!-- FEATURED PRODUCT -->
        <div class="section product-item si-featured">
            <div class="container">
                <div class="row row-title">
                    <div class="col s12">
                        <div class="section-title">
                            <span class="theme-secondary-color">OUR</span> PRODUCTS
                        </div>
                    </div>
                </div>
                <div class="row slick-product">
                    <div class="col s12">
                        <div id="featured-product" class="featured-product">
                            <!-- Product item-->
                            <div>
                                <div class="col-slick-product">
                                    <div class="box-product">
                                        <div class="bp-top">
                                            <div class="product-list-img">
                                                <div class="pli-one">
                                                    <div class="pli-two">
                                                        <img src="img/product.jpg" alt="img">
                                                    </div>
                                                </div>
                                            </div>
                                            <h5><a href="#">Electric Toothbrus</a></h5>
                                            <div class="item-info">Oral-B Vitality White and Clean Rechargeable Electric Toothbrus</div>
                                            <div class="price">
                                                Rs. 1,299
                                            </div>
                                        </div>
                                        <div class="bp-bottom">
                                            <a href="product-page.html">
                                                <button class="btn button-add-cart">BUY</button></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- End Product item-->
                            <!-- Product item-->
                            <div>
                                <div class="col-slick-product">
                                    <div class="box-product">
                                        <div class="bp-top">
                                            <div class="product-list-img">
                                                <div class="pli-one">
                                                    <div class="pli-two">
                                                        <img src="img/product2.jpg" alt="img">
                                                    </div>
                                                </div>
                                            </div>
                                            <h5><a href="#">Kids Electric Rechargeable</a></h5>
                                            <div class="item-info">Oral-B Kids Electric Rechargeable Star War Toothbrush - Red & Blue </div>
                                            <div class="price">
                                                Rs. 1183
                                            </div>
                                        </div>
                                        <div class="bp-bottom">
                                            <a href="#">
                                                <button class="btn button-add-cart">BUY</button></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- End Product item-->
                            <!-- Product item-->
                            <div>
                                <div class="col-slick-product">
                                    <div class="box-product">
                                        <div class="bp-top">
                                            <div class="product-list-img">
                                                <div class="pli-one">
                                                    <div class="pli-two">
                                                        <img src="img/product3.jpg" alt="img">
                                                    </div>
                                                </div>
                                            </div>
                                            <h5><a href="#">Mouthwash</a></h5>
                                            <div class="item-info">P&G Crest Pro-Health Mouthwash Advanced Extra Deep Clean 36mL</div>
                                            <div class="price">Rs. 250</div>
                                        </div>
                                        <div class="bp-bottom">
                                            <a href="#">
                                                <button class="btn button-add-cart">BUY</button></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- End Product item-->
                            <!-- Product item-->
                            <div>
                                <div class="col-slick-product">
                                    <div class="box-product">
                                        <div class="bp-top">
                                            <div class="product-list-img">
                                                <div class="pli-one">
                                                    <div class="pli-two">
                                                        <img src="img/product4.jpg" alt="img">
                                                    </div>
                                                </div>
                                            </div>
                                            <h5><a href="#">Dental Floss</a></h5>
                                            <div class="item-info">Oral-B Glide Pro-Health Deep Clean Dental Floss, Cool Mint</div>
                                            <div class="price">Rs. 99</div>
                                        </div>
                                        <div class="bp-bottom">
                                            <a href="product-page.html">
                                                <button class="btn button-add-cart">BUY</button></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <div class="col-slick-product">
                                    <div class="box-product">
                                        <div class="bp-top">
                                            <div class="product-list-img">
                                                <div class="pli-one">
                                                    <div class="pli-two">
                                                        <img src="img/product4.jpg" alt="img">
                                                    </div>
                                                </div>
                                            </div>
                                            <h5><a href="#">Dental Floss</a></h5>
                                            <div class="item-info">Oral-B Glide Pro-Health Deep Clean Dental Floss, Cool Mint</div>
                                            <div class="price">Rs. 99</div>
                                        </div>
                                        <div class="bp-bottom">
                                            <a href="product-page.html">
                                                <button class="btn button-add-cart">BUY</button></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="more-product-list">
                            <a class="more-btn" href="product-list.html">See More Product ></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END FEATURED PRODUCT -->


        <!-- TESTIMONIAL  -->
        <div class="section testimonial">
            <div class="testimonial-wrap">
                <div class="container">
                    <div class="row">
                        <div class="col s12">
                            <div class="section-title">
                                CUSTOMER REVIEWS
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="wrap-item-testimonial">
                            <div id="testimonial-item" class="owl-carousel">
                                <!-- item -->
                                <div class="item">
                                    <div class="item-testimonial">
                                        <div class="client-info">
                                            <div class="client-img">
                                                <img src="img/profile6.jpg" alt="profile">
                                            </div>
                                            <div class="rating">
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                            </div>
                                        </div>
                                        <div class="client-content">
                                            <p>
                                                Best Service, Good Luck With Sale!
                                            </p>
                                            <div class="client-title">
                                                <h4>John Doe</h4>
                                                <h5>Photographer</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end item -->
                                <!-- item -->
                                <div class="item">
                                    <div class="item-testimonial">
                                        <div class="client-info">
                                            <div class="client-img">
                                                <img src="img/profile5.jpg" alt="profile">
                                            </div>
                                            <div class="rating">
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                            </div>
                                        </div>
                                        <div class="client-content">
                                            <p>
                                                Best place and fast respond, Thank You!
                                            </p>
                                            <div class="client-title">
                                                <h4>Elly Doe</h4>
                                                <h5>Entrepreneur</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end item -->
                                <!-- item -->
                                <div class="item">
                                    <div class="item-testimonial">
                                        <div class="client-info">
                                            <div class="client-img">
                                                <img src="img/profile4.jpg" alt="profile">
                                            </div>
                                            <div class="rating">
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                                <span class="fa fa-star"></span>
                                            </div>
                                        </div>
                                        <div class="client-content">
                                            <p>
                                                Good Product, I like this..
                                            </p>
                                            <div class="client-title">
                                                <h4>Tayoo Doe</h4>
                                                <h5>Driver</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end item -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="background-testimonial"></div>
        </div>
        <!-- END TESTIMONIAL  -->

        <!-- FOOTER  -->
        <footer id="footer" style="background: #fff;">
            <div class="container">
                <div class="row copyright bluefnt">
                    Design & Developed by <span>SFA Technologies</span>.
                </div>
            </div>
        </footer>
    </form>
    <!-- Script -->
    <script src="js/jquery.min.js"></script>
    <script src="js/materialize.min.js"></script>
    <!-- Owl carousel -->
    <script src="lib/owlcarousel/owl.carousel.min.js"></script>
    <!-- Magnific Popup core JS file -->
    <script src="lib/Magnific-Popup-master/dist/jquery.magnific-popup.js"></script>
    <!-- Slick JS -->
    <script src="lib/slick/slick/slick.min.js"></script>
    <!-- Custom script -->
    <script src="js/custom.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.popup-with-form').magnificPopup({
                type: 'inline',
                preloader: false,
                focus: '#name',

                // When elemened is focused, some mobile browsers in some cases zoom in
                // It looks not nice, so we disable it:
                callbacks: {
                    beforeOpen: function () {
                        if ($(window).width() < 700) {
                            this.st.focus = false;
                        } else {
                            this.st.focus = '#name';
                        }
                    }
                }
            });
        });
    </script>
</body>
</html>
