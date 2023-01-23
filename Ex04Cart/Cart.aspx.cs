using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ex04Cart
{
    public partial class Cart : System.Web.UI.Page
    {
        private CartItemList cart;

        protected void Page_Load(object sender, EventArgs e)
        {
            cart = CartItemList.GetCart();
            if (!IsPostBack)          //if this is not the result of a post back (button was not clicked)
                this.DisplayCart();
        }

        private void DisplayCart()
        {
            lstCart.Items.Clear();         //listCart is the list box display in the design view
            CartItem item;
            for (int i = 0; i < cart.Count; i++)    //add the items in the cart to the listCart for display
            {
                item = cart[i];
                lstCart.Items.Add(item.Display());
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)     //remove selected item in list box display from cart
        {
            if (cart.Count > 0)
            {
                if (lstCart.SelectedIndex > -1)
                {
                    cart.RemoveAt(lstCart.SelectedIndex);
                    this.DisplayCart();
                }
                else
                {
                    lblMessage.Text = "Please select the item you want to remove.";
                }
            }
        }

        protected void btnEmpty_Click(object sender, EventArgs e)      //clear both the cart and listCart
        {
            if (cart.Count > 0)
            {
                cart.Clear();
                lstCart.Items.Clear();
            }
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CheckOut.aspx");    //go to the CheckOut page
        }
    }
}