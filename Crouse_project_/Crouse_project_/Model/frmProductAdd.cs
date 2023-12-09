using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crouse_project_.Model
{
    public partial class frmProductAdd : SampleAdd
    {
        public frmProductAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        public int cID = 0;

        private void frmProductAdd_Load(object sender, EventArgs e)
        {
            //for cb fill
            string qry = "Select catID 'id' , catName 'name' from category";

            MainClass.CBFill(qry, cbCat);

            if (cID > 0)// For update
            {
                cbCat.SelectedValue = cID;
            }
        }
        string filePath;
        Byte[] imageByteArray;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png)|* .png; *.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                txtImage.Image = new Bitmap(filePath);
            }
        }
        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";

            if (id == 0) //Insert
            {
                qry = "Insert into products Values(@Name, @Price,@cat,@img)";
            }
            else //Update
            {
                qry = "Update products Set pName = @Name , pPrice = @price, CategoryID = @cat , pImage = @img where pID = @id ";
            }

            //For image
            Image temp = new Bitmap(txtImage.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imageByteArray = ms.ToArray();

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@Price", txtPrice.Text);
            ht.Add("@cat", Convert.ToInt32(cbCat.SelectedValue));
            ht.Add("@img", imageByteArray);

            if (MainClass.SQL(qry, ht) > 0)
            {
                guna2MessageDialog1.Show("Saved succesfully...");
                id = 0;
                txtName.Text = "";
                txtPrice.Text = "";
                cbCat.SelectedIndex = -1;
                txtName.Focus();
            }



        }
    }
}
