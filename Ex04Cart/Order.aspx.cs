using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ex04Cart
{
    public partial class Order : System.Web.UI.Page
    {

        private Product selectedProduct;    //represents the selected product in dropdown list - Product class defined in Models/Product.cs

        protected void Page_Load(object sender, EventArgs e)
        {
            //bind dropdown on first load; get and show product data on every load   
            if (!IsPostBack) ddlProducts.DataBind();   //binds the SQL Data Source to drop down list on first page load

            selectedProduct = this.GetSelectedProduct();    //set selectedProduct to the one selected in drop down list
            lblName.Text = selectedProduct.Name;
            lblShortDescription.Text = selectedProduct.ShortDescription;
            lblLongDescription.Text = selectedProduct.LongDescription;
            lblUnitPrice.Text = selectedProduct.UnitPrice.ToString("c") + " each";
            imgProduct.ImageUrl = "Images/Products/" + selectedProduct.ImageFile;
        }


        private Product GetSelectedProduct()
        {
            //get row from AccessDataSource based on value in dropdownlist
            DataView productsTable = (DataView)
                Halloween.Select(DataSourceSelectArguments.Empty);
            productsTable.RowFilter =
                "ProductID = '" + ddlProducts.SelectedValue + "'";
            DataRowView row = (DataRowView)productsTable[0];

            //create a new Product object and load with data from row. Returns the Product to the calling function. 
            Product p = new Product();
            p.ProductID = row["ProductID"].ToString();
            p.Name = row["Name"].ToString();
            p.ShortDescription = row["ShortDescription"].ToString();
            p.LongDescription = row["LongDescription"].ToString();
            p.UnitPrice = (decimal)row["UnitPrice"];
            p.ImageFile = row["ImageFile"].ToString();
            return p;
        }


        //"Add to Cart" button event handler - adds selected item to the cart - first checks if it is already in cart
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //get cart from session and selected item from cart
                CartItemList cart = CartItemList.GetCart();
                CartItem cartItem = cart[selectedProduct.ProductID];

                //if item isn’t in cart, add it; otherwise, increase its quantity
                if (cartItem == null)
                {
                    cart.AddItem(selectedProduct,
                                 Convert.ToInt32(txtQuantity.Text));
                }
                else
                {
                    cartItem.AddQuantity(Convert.ToInt32(txtQuantity.Text));
                }
                Response.Redirect("Cart.aspx");
            }
        }
    }
}