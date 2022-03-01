using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_only_Sun_Resort_Reservation
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// Set min and max for the date boxes
			dtpCheckIn.Value = DateTime.Today.AddDays(1);
			dtpCheckOut.Value = DateTime.Today.AddDays(8);
			dtpCheckIn.MinDate = DateTime.Today;
			dtpCheckOut.MinDate = DateTime.Today;
			dtpCheckIn.MaxDate = DateTime.Today.AddYears(1);
			dtpCheckOut.MaxDate = DateTime.Today.AddYears(1);

			//data for the list box
			lstCredutCard.Items.Add("Visa");
			lstCredutCard.Items.Add("Master Card");
			lstCredutCard.Items.Add("American Express");
		}

		//I'm not sure if I'm understanding this one correctly
		private void btnReserve_Click(object sender, EventArgs e)
		{
			//Check the name and bed type data
			errProvider.Clear();
			if (txtName.Text == string.Empty){
				errProvider.SetError(txtName, "Name required");
				txtName.Focus();
			}
			else if (!radKing.Checked && !radDouble.Checked && !radQueen.Checked){
				errProvider.SetError(grpBedSize, "Bed size required");
			}
			else
			{
				try {
					// Part 2
					RoomRate RoomCosts = new RoomRate();

					if (radKing.Checked)
					{
						RoomCosts.BedType = BedType.King;
					}
					else if (radQueen.Checked)
					{
						RoomCosts.BedType = BedType.Queen;
					}
					else if (radDouble.Checked)
					{
						RoomCosts.BedType = BedType.Double;
					}

					RoomCosts.CheckIn = dtpCheckIn.Value;
					RoomCosts.CheckOut = dtpCheckOut.Value;

					if (chkMember.Checked) 
					{
						RoomCosts.Member = true;
					}

					RoomCosts.Name = txtName.Text;
					RoomCosts.GetTotal();
					txtDisplay.Text = RoomCosts.ToString();

				} catch (Exception ex){
					MessageBox.Show(ex.Message, "Data Error");
				}
			}
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void mnuAbout_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sun Resorts is a vacation paradise located on blue waters", "Sun Resorts");
		}

		private void mnuContactUs_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Contact us at sresorts@ccri.edu", "Sun Resorts");
		}

        private void dtpCheckIn_ValueChanged(object sender, EventArgs e)
        {
			dtpCheckOut.Value = dtpCheckIn.Value.AddDays(1);
			dtpCheckOut.MaxDate = dtpCheckOut.Value.AddYears(1);
        }
    }
}
