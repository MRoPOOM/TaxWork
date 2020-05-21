using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            relationship.Enabled = false;
            pregnant.Enabled = false;
            mychild.Enabled = false;
            relationshipbox.Enabled = false;
        }

        double taxgroup1,taxgroup2,taxgroup3,taxgroup4,taxgroup5,yearsal,costs,sumoftax,taxre,sumofgroup4, sumofgroup4part2;


        private void relations_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == relations)
            {
                if(nocouple.Enabled == true)
                {
                    relationship.Enabled = false;
                    pregnant.Enabled = false;
                    mychild.Enabled = false;
                    nocouple.Checked = false;
                    couplemoney.Checked = false;
                    couplenomoney.Checked = false;
                }
                else
                {
                    relationship.Enabled = true;
                    pregnant.Enabled = true;
                    mychild.Enabled = true;
                }
            }
            else if (sender == single || sender == other)
            {
                nocouple.Checked = false;
                couplemoney.Checked = false;
                couplenomoney.Checked = false;
                relationship.Enabled = false;
                pregnant.Enabled = false;
                mychild.Enabled = false;
                relationshipbox.Enabled = false;
            }
        }

        private void couplenomoney_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == couplenomoney)
            {
                if (couplenomoney.Checked)
                {
                    relationship.Enabled = true;
                    pregnant.Enabled = true;
                    mychild.Enabled = true;
                    relationshipbox.Enabled = true;
                }
                if (couplemoney.Checked)
                {
                    relationship.Enabled = true;
                    pregnant.Enabled = true;
                    mychild.Enabled = true;
                    relationshipbox.Enabled = false;
                }
                if (nocouple.Checked)
                {
                    relationship.Enabled = true;
                    pregnant.Enabled = true;
                    mychild.Enabled = true;
                    relationshipbox.Enabled = false;
                }
            }
        }

        private void single_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == single)
            {
                nocouple.Checked = false;
                couplemoney.Checked = false;
                couplenomoney.Checked = false;
                relationship.Enabled = false;
                pregnant.Enabled = false;
                mychild.Enabled = false;
                relationshipbox.Enabled = false;
            }
        }

        private void other_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == other)
            {
                nocouple.Checked = false;
                couplemoney.Checked = false;
                couplenomoney.Checked = false;
                relationship.Enabled = false;
                pregnant.Enabled = false;
                mychild.Enabled = false;
                relationshipbox.Enabled = false;
            }
        }


        ///คำนวณ
        private void calculatebutton_Click(object sender, EventArgs e)
        {
            salary_TextChanged(sender, e);
            savelife(sender, e);
            housepayment_ValueChanged(sender, e);
            shopshare_ValueChanged(sender, e);
            if (sumoftax >= 0 && sumoftax < 150000)
            {
                sumoftax = 0;
            }
            else if (sumoftax > 150000 && sumoftax <= 300000)
            {
                sumoftax = (sumoftax - 150000) * 0.05;
            }
            else if (sumoftax > 300000 && sumoftax <= 500000)
            {
                sumoftax = (sumoftax - 300000) * 0.1 + 7500;
            }
            else if (sumoftax > 500000 && sumoftax <= 750000)
            {
                sumoftax = (sumoftax - 500000) * 0.15 + 27500;
            }
            else if (sumoftax > 750000 && sumoftax <= 1000000)
            {
                sumoftax = (sumoftax - 750000) * 0.2 + 65000;
            }
            else if (sumoftax > 1000000 && sumoftax <= 2000000)
            {
                sumoftax = (sumoftax - 1000000) * 0.25 + 115000;
            }
            else if (sumoftax > 2000000 && sumoftax <= 5000000)
            {
                sumoftax = (sumoftax - 2000000) * 0.3 + 365000;
            }
            else if (sumoftax > 5000000)
            {
                sumoftax = (sumoftax - 5000000) * 0.35 + 1265000;
            }
            costtax.Text = sumoftax.ToString();
            salary.Focus();
            salary.Select(salary.Text.Length, 0);
        }

        //แก้บัคให้ใส่ได้แค่เฉพาะตัวเลข
        private void salary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        //// กลุ่มภาษีกลุ่มที่ 1
        private void salary_TextChanged(object sender, EventArgs e)
        {
            taxgroup1 = 0;
            ///สถานภาพ
            if (single.Checked)
            {
                taxgroup1 += 60000;
            }
            else if(relations.Checked)
            {
                if (nocouple.Checked)
                {
                    taxgroup1 += 60000;
                }
                else if (couplemoney.Checked)
                {
                    taxgroup1 += 60000;
                }
                else
                {
                    taxgroup1 += 120000;
                }
            }
            else
            {
                taxgroup1 += 60000;
            }
            //// พ่อแม่ตัวเอง
            if (fatherparent.Checked)
            {
                taxgroup1 += 30000;
            }
            if (motherparent.Checked)
            {
                taxgroup1 += 30000;
            }
            else
            {
                taxgroup1 += 0;
            }
            //// พ่อแม่คู่สมรส
            if (fatherparentcouple.Checked)
            {
                taxgroup1 += 30000;
            }
            if (motherparentcouple.Checked)
            {
                taxgroup1 += 30000;
            }
            else
            {
                taxgroup1 += 0;
            }
            //// ลูกก่อน 61
            int tempchildafter61;
            int numbericchildafter61 = Convert.ToInt32(childafter2561.Value);
            if (childafter2561.Value > 0)
            {
                if (childbefore2561.Value == 0)
                {
                    tempchildafter61 = ((numbericchildafter61 - 1) * 60000 + 30000);
                }
                else
                {
                    tempchildafter61 = numbericchildafter61 * 60000;
                }
            }
            else
            {
                tempchildafter61 = 0;
            }
            taxgroup1 += tempchildafter61;
            int tempchildbefore61 = Convert.ToInt32(childbefore2561.Value) * 30000;
           
            ///ค่าฝากครรภ์
            int preg = Convert.ToInt32(pregnant.Value);
            if(pregnant.Text == "")
            {
                pregnant.Text = "0";
            }
            ///ค่าบาดเจ็บ
            int inj = Convert.ToInt32(injured.Value) * 60000;
            if (injured.Text == "")
            {
                injured.Text = "0";
            }

            ///รวมกลุ่มที่ 1
            taxgroup1 = taxgroup1 + inj + preg + tempchildbefore61;

            ////แก้บัคไม่ใส่ตัวเลขเงินเดือน
            if (salary.Text == "")
            {
                MessageBox.Show("กรุณากรอกจำนวนเงินเดือนก่อนใช้งาน", "Salary Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                salary.Text = "0";
            }
            ////คำนวณรายได้สุทธิ
            yearsal = Convert.ToDouble(salary.Text) * 12; ///เงินเดือน * 12 
            ///ค่าใช้จ่าย
            costs = yearsal * 0.5; ///เงินเดือนหักออก 50% เป็นค่าใช้จ่ายแต่ไม่เกิน 100K
            if (costs > 100000)
            {
                costs = 100000;
            }
            yearsalary.Text = yearsal.ToString();
            taxre = taxgroup1 + taxgroup2 + taxgroup3 + taxgroup5;
            sumoftax = (yearsal - costs - taxre) - taxgroup4;
            if (sumoftax < 0)
            {
                sumoftax = 0;
            }
            price.Text = sumoftax.ToString();
        }

        //// กลุ่มภาษีกลุ่มที่ 2
        private void savelife(object sender, EventArgs e)
        {
            taxgroup2 = 0;
            ////15%
            double discount15 = yearsal * 0.15;
            ////ประกันสังคม
            int social_pragun = Convert.ToInt32(socialsave.Value);
            if (social_pragun >= 9000)
            {
                social_pragun = 9000;
            }
            if (socialsave.Text == "")
            {
                socialsave.Text = "0";
            }

            ////ประกันชีวิต
            int life_pragun = Convert.ToInt32(lifesave.Value);
            if (life_pragun >= 100000)
            {
                life_pragun = 100000;
            }
            if (lifesave.Text == "")
            {
                lifesave.Text = "0";
            }

            ////ประกันสุขภาพ
            int body_pragun = Convert.ToInt32(bodysave.Value);
            if (body_pragun >= 15000)
            {
                body_pragun = 15000;
            }
            if (bodysave.Text == "")
            {
                bodysave.Text = "0";
            }

            int praguntotal = life_pragun + body_pragun;
            
            ///รวมประกันสุขภาพ + ชีวิต
            if (praguntotal >= 100000)
            {
                praguntotal = 100000;
            }


            ////เบี้ยสุขภาพบิดามารดา
            int dadandmom = Convert.ToInt32(dadmomsave.Value);
            if (dadandmom >= 15000)
            {
                dadandmom = 15000;
            }
            if (dadmomsave.Text == "")
            {
                dadmomsave.Text = "0";
            }

            ////คู่สมรส
            int mycouple = Convert.ToInt32(couplesave.Value);
            if (mycouple >= 10000)
            {
                mycouple = 10000;
            }
            if (couplesave.Text == "")
            {
                couplesave.Text = "0";
            }

            ////เงินกองทุนสำรองเลี้ยงชีพ
            int spare = Convert.ToInt32(sparelifesave.Value);
            if (sparelifesave.Text == "")
            {
                sparelifesave.Text = "0";
            }
            if (discount15 > 490000 && spare > 10000)
            {
                sparelifesave.Maximum = 490000;
            }
            else if (discount15 < 490000 && spare > discount15)
            {
                sparelifesave.Maximum = Convert.ToInt32(discount15);
            }
            else if (discount15 > 490000 && spare < discount15)
            {
                sparelifesave.Maximum = 490000;
            }
            else 
            {
                sparelifesave.Maximum = 500000;
            }

            ////เบี้ยเงินสะสมกองทุน
            int stack = Convert.ToInt32(stackmoneysave.Value);
            if (stackmoneysave.Text == "")
            {
                stackmoneysave.Text = "0";
            }
            if (discount15 > 500000)
            {
                stackmoneysave.Maximum = 500000;
            }
            else
            {
                stackmoneysave.Maximum = Convert.ToInt32(discount15);
            }

            ////เงินสะสม กอช.
            int stackthai = Convert.ToInt32(thstack.Value);

            ///เงินบำนาญ
            int moneypermanent = Convert.ToInt32(premiumlifesave.Value);
            if (premiumlifesave.Text == "")
            {
                premiumlifesave.Text = "0";
            }
            if (discount15 > 200000)
            {
                premiumlifesave.Maximum = 200000;
            }
            else
            {
                premiumlifesave.Maximum = Convert.ToInt32(discount15);
            }

            ///LTF
            int ltf = Convert.ToInt32(LTFBuy.Value);
            if (LTFBuy.Text == "")
            {
                LTFBuy.Text = "0";
            }
            if (discount15 > 500000)
            {
                LTFBuy.Maximum = 500000;
            }
            else
            {
                LTFBuy.Maximum = Convert.ToInt32(discount15);
            }

            ///RMF
            int rmf = Convert.ToInt32(RMFBuy.Value);
            if (RMFBuy.Text == "")
            {
                RMFBuy.Text = "0";
            }
            if (discount15 > 500000)
            {
                RMFBuy.Maximum = 500000;
            }
            else
            {
                RMFBuy.Maximum = Convert.ToInt32(discount15);
            }

            ///รวม
            int sumstackmoney = moneypermanent + spare + stack + rmf;
            if(sumstackmoney > 500000)
            {
                sumstackmoney = 500000;
            }
            else
            {
                sumstackmoney = moneypermanent + spare + stack + rmf;
            }

            taxgroup2 = sumstackmoney + social_pragun + praguntotal + ltf + mycouple + dadandmom + stackthai;
        }

        //// กลุ่มภาษีกลุ่มที่ 3
        private void housepayment_ValueChanged(object sender, EventArgs e)
        {
            taxgroup3 = 0;
            int housepaid = Convert.ToInt32(housepayment.Value);
            if (housepayment.Text == "")
            {
                housepayment.Text = "0";
            }
            double firsthouse58 = (Convert.ToInt32(firsthousebefore58.Value)) * 0.04;
            if (firsthousebefore58.Text == "")
            {
                firsthousebefore58.Text = "0";
            }
            double firsthouse62 = (Convert.ToInt32(firsthouseafter62.Value)) * 0.04;
            if (firsthouseafter62.Text == "")
            {
                firsthouseafter62.Text = "0";
            }
            taxgroup3 = housepaid + firsthouse58 + firsthouse62;
        }

        //// กลุ่มภาษีกลุ่มที่ 4
        private void supportanything_ValueChanged(object sender, KeyEventArgs e)
        {
            taxgroup4 = 0;
            sumofgroup4 = yearsal - costs - taxre;
            double sharemoney,sharemoneynormal, sharemoneytotu = 0;
            double sup10per = sumofgroup4 * 0.1;
            double supporter10per = (sumofgroup4 * 0.1) / 2;
            if (supportany.Text == "")
            {
                supportany.Text = "0";
            }
            sharemoney = Convert.ToInt32(supportany.Text) * 2;
            if (sharemoney > sup10per)
            {
                sharemoney = sup10per;
                supportany.Text = supporter10per.ToString(); 
            }
            if (supportnor.Text == "")
            {
                supportnor.Text = "0";
            }
            sharemoneynormal = Convert.ToDouble(supportnor.Text);
            sumofgroup4part2 = yearsal - costs - taxre - sharemoney;
            double sup10peradvanced = sumofgroup4part2 * 0.1;
            if (sharemoneynormal > sup10peradvanced)
            {
                sharemoneynormal = sup10peradvanced;
                supportnor.Text = sup10peradvanced.ToString();
            }
            if (supportt.Text == "")
            {
                supportt.Text = "0";
            }
            sharemoneytotu = Convert.ToDouble(supportt.Text);
            if (sharemoneytotu > 10000)
            {
                sharemoneytotu = 10000;
                supportt.Text = sharemoneytotu.ToString();
            }
            ///บริจาคเงินทั่วไป + บริจาครัฐบาล
            taxgroup4 = sharemoneynormal + sharemoney + sharemoneytotu;
        }

        /// กลุ่มภาษีกลุ่มที่ 5
        private void shopshare_ValueChanged(object sender, EventArgs e)
        {
            taxgroup5 = 0;
            ///ช๊อปช่วยชาติ
            int nationhelp = Convert.ToInt32(shopshare.Value);
            if (nationhelp > 15000)
            {
                nationhelp = 15000;
            }
            if (shopshare.Text == "")
            {
                shopshare.Text = "0";
            }
            ///การกีฬา
            int sporthelp = Convert.ToInt32(sportshare.Value);
            if (sporthelp > 15000)
            {
                sporthelp = 15000;
            }
            if (sportshare.Text == "")
            {
                sportshare.Text = "0";
            }
            ///หนังสือ
            int bookhelp = Convert.ToInt32(buybook.Value);
            if (bookhelp > 15000)
            {
                bookhelp = 15000;
            }
            if (buybook.Text == "")
            {
                buybook.Text = "0";
            }
            ///โอท็อป
            int otophelp = Convert.ToInt32(buyotop.Value);
            if (otophelp > 15000)
            {
                otophelp = 15000;
            }
            if (buyotop.Text == "")
            {
                buyotop.Text = "0";
            }
            ///เมืองหลวง
            int mtravel = Convert.ToInt32(metropolistravel.Value);
            if (mtravel > 15000)
            {
                mtravel = 15000;
            }
            if (metropolistravel.Text == "")
            {
                metropolistravel.Text = "0";
            }
            ///เมืองรอง
            int ntravel = Convert.ToInt32(normaltravel.Value);
            if (ntravel > 20000)
            {
                ntravel = 20000;
            }
            if (normaltravel.Text == "")
            {
                normaltravel.Text = "0";
            }
            ///รวมเมืองหลวง+รอง
            int mntravel = mtravel + ntravel;
            if (mntravel > 20000)
            {
                mntravel = 20000;
            }
            ///บ้าน
            int house = Convert.ToInt32(houserepair.Value);
            if (house > 100000)
            {
                house = 100000;
            }
            if (houserepair.Text == "")
            {
                houserepair.Text = "0";
            }
            ///รถ
            int car = Convert.ToInt32(carrepair.Value);
            if (car > 30000)
            {
                car = 30000;
            }
            if (carrepair.Text == "")
            {
                carrepair.Text = "0";
            }
            ///รวมบ้าน+รถ
            int chrepair = house + car;
            if (chrepair > 100000)
            {
                chrepair = 100000;
            }
            taxgroup5 = nationhelp + sporthelp + bookhelp + otophelp + mntravel + chrepair;
        }

        /// รีเซ็คค่า
        private void resetvalue_Click_1(object sender, EventArgs e)
        {
            single.Checked = false;
            relations.Checked = false;
            other.Checked = false;
            fatherparent.Checked = false;
            motherparent.Checked = false;
            fatherparentcouple.Checked = false;
            motherparentcouple.Checked = false;
            nocouple.Checked = false;
            couplemoney.Checked = false;
            couplenomoney.Checked = false;
            costtax.Text = "0";
            salary.Text = "0";
            pregnant.Text = "0";
            pregnant.Value = 0;
            injured.Text = "0";
            injured.Value = 0;
            childbefore2561.Text = "0";
            childbefore2561.Value = 0;
            childafter2561.Text = "0";
            childafter2561.Value = 0;
            socialsave.Text = "0";
            socialsave.Value = 0;
            lifesave.Text = "0";
            lifesave.Value = 0;
            bodysave.Text = "0";
            bodysave.Value = 0;
            dadmomsave.Text = "0";
            dadmomsave.Value = 0;
            couplesave.Text = "0";
            couplesave.Value = 0;
            sparelifesave.Text = "0";
            sparelifesave.Value = 0;
            stackmoneysave.Text = "0";
            stackmoneysave.Value = 0;
            thstack.Text = "0";
            thstack.Value = 0;
            premiumlifesave.Text = "0";
            premiumlifesave.Value = 0;
            LTFBuy.Text = "0";
            LTFBuy.Value = 0;
            RMFBuy.Text = "0";
            RMFBuy.Value = 0;
            housepayment.Text = "0";
            housepayment.Value = 0;
            firsthousebefore58.Text = "0";
            firsthousebefore58.Value = 0;
            firsthouseafter62.Text = "0";
            firsthouseafter62.Value = 0;
            supportany.Text = "0";
            supportnor.Text = "0";
            supportt.Text = "0";
            shopshare.Text = "0";
            shopshare.Value = 0;
            sportshare.Text = "0";
            sportshare.Value = 0;
            buybook.Text = "0";
            buybook.Value = 0;
            buyotop.Text = "0";
            buyotop.Value = 0;
            metropolistravel.Text = "0";
            metropolistravel.Value = 0;
            normaltravel.Text = "0";
            normaltravel.Value = 0;
            houserepair.Text = "0";
            houserepair.Value = 0;
            carrepair.Text = "0";
            carrepair.Value = 0;
            taxre = 0;
            taxgroup4 = 0;
        }
    }
}
