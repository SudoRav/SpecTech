using SpecTechUnitTests;

using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using Microsoft.Analytics.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MaterialSkinExample;

namespace SpecTechUnitTests
{
    [TestClass]
    public class SpecTechTests
    {
        [TestMethod]
        public void n01_Login_log1_pass1_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            string log = "1";
            string pass = "1";

            //act
            result = DBF.Login(log, pass);

            //assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void n02_Login_log0_pass0_falsereturn()
        {
            //arrage
            bool expected = false;
            bool result;

            string log = "0";
            string pass = "0";

            //act
            result = DBF.Login(log, pass);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n03_GetInfoUser_ID1()
        {
            //arrage
            string[] expected = new string[] { "1", "1", "1", "1", "1", "No post" };
            string[] result;

            string id = "1";

            //act
            result = DBF_Users.GetInfoUser(id);

            //assert
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);
            Assert.AreEqual(expected[2], result[2]);
            Assert.AreEqual(expected[3], result[3]);
            Assert.AreEqual(expected[4], result[4]);
            Assert.AreEqual(expected[5], result[5]);
        }

        [TestMethod]
        public void n04_validLog_log1_falsereturn()
        {
            //arrage
            bool expected = false;
            bool result;

            string log = "1";

            //act
            result = DBF_Users.validLog(log);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n05_validLog_log1_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            string log = "0";

            //act
            result = DBF_Users.validLog(log);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n06_crtUser_log4_pass4_f4_i4_phone4_email4_post1()
        {
            //arrage
            bool expected = true;
            bool result;

            string log = "4";
            string pass = "4";
            string F = "4";
            string I = "4";
            string phone = "4";
            string email = "4";
            int ID_post = 1;

            //act
            result = DBF_Users.crtUser(log, pass, F, I, phone, email, ID_post);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n07_updUser_log4_pass4_f41_i41_phone41_email41_post1()
        {
            //arrage
            bool expected = true;
            bool result;

            StatUser.ID = 4;
            string log = "4";
            string pass = "4";
            string F = "41";
            string I = "41";
            string phone = "41";
            string email = "41";
            int ID_post = 2;

            //act
            result = DBF_Users.updUser(F, I, phone, email, ID_post);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n08_crtPost_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            string postName = "testPost";
            bool access1 = true;
            bool access2 = true;
            bool access3 = true;
            bool access4 = true;
            bool access5 = true;
            bool access6 = true;
            int salary = 100;

            //act
            result = DBF_Users.crtPost(postName, access1, access2, access3, access4, access5, access6, salary);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n09_updPost_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int id = 4;
            string postName = "testPost";
            bool access1 = true;
            bool access2 = false;
            bool access3 = true;
            bool access4 = false;
            bool access5 = true;
            bool access6 = false;
            int salary = 100;

            //act
            result = DBF_Users.updPost(id, postName, access1, access2, access3, access4, access5, access6, salary);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n10_delPost_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            string id = "4";
            string postName = "testPost";
            bool access1 = true;
            bool access2 = true;
            bool access3 = true;
            bool access4 = true;
            bool access5 = true;
            bool access6 = true;
            int salary = 100;

            //act
            result = DBF_Users.delPost(id);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n11_crtAbsence_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int ID_user = 4;
            string cause = "Причина 1";
            string date_start = "2020-12-30";
            string date_fin = "2020-12-30";

            //act
            result = DBF_Users.crtAbsence(ID_user, cause, date_start, date_fin);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n12_crtEducation_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int ID_user = 4;
            string education = "Образование";

            //act
            result = DBF_Users.crtEducation(ID_user, education);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n13_crtPay_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int ID_user = 4;
            string date = "2020-12-30";
            int summ = 400;

            //act
            result = DBF_Users.crtPay(ID_user, date, summ);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n14_crtPrize_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int ID_user = 4;
            string date = "2020-12-30";
            int summ = 400;

            //act
            result = DBF_Users.crtPrize(ID_user, date, summ);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n15_delUser_id4_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            string id = "4";
            string log = "4";
            string pass = "4";
            string F = "41";
            string I = "41";
            string phone = "41";
            string email = "41";
            int ID_post = 2;

            //act
            result = DBF_Users.delUser(id);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n16_crtTech_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            string name = "Техника 3";
            int ID_type = 1;
            string filePath = @"C:\Users\selme\Desktop\1.png";
            string desc = "Описание";
            int price = 1000;

            //act
            result = DBF_Tech.crtTech(name, ID_type, filePath, desc, price);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n17_updTech_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            StatTech.ID = 3;
            int id = 3;
            string name = "Техника 3";
            int ID_type = 2;
            string filePath = @"C:\Users\selme\OneDrive\Рабочий стол\1.png";
            string desc = "Описание";
            int price = 2000;

            //act
            result = DBF_Tech.updTech(name, ID_type, filePath, desc, price);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n18_setDiscount_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int ID_tech = 3;
            int discount = 50;

            //act
            result = DBF_Tech.setDiscount(ID_tech, discount);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n19_crtRep_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int ID_tech = 3;
            string text_rep = "колесо";

            //act
            result = DBF_Tech.crtRep(ID_tech, text_rep);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n20_delRep_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int ID_tech = 3;

            //act
            result = DBF_Tech.delRep(ID_tech);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n21_detailTech_truereturn()
        {
            //arrage
            string[] expected = new string[] { "3", "Техника 3", "Type1", "Описание", "2000", "False", "False", "", "50" };
            string[] result;

            int id = 3;

            //act
            result = DBF_Tech.detail(id);

            //assert
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);
            Assert.AreEqual(expected[2], result[2]);
            Assert.AreEqual(expected[3], result[3]);
            Assert.AreEqual(expected[4], result[4]);
            Assert.AreEqual(expected[5], result[5]);
            Assert.AreEqual(expected[6], result[6]);
            Assert.AreEqual(expected[7], result[7]);
            Assert.AreEqual(expected[8], result[8]);
        }

        [TestMethod]
        public void n22_crtCharacts_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            StatTech.ID = 3;
            string name = "вес";
            string val = "100";

            //act
            result = DBF_Tech.crtCharacts(name, val);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n23_delCharacts_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int id = 4;

            //act
            result = DBF_Tech.delCharacts(id);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n24_crtType_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            StatTech.ID = 3;
            string typeName = "new type";

            //act
            result = DBF_Tech.crtType(typeName);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n25_delType_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int id = 4;

            //act
            result = DBF_Tech.delType(id);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n26_delTech_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int id = 4;

            //act
            result = DBF_Tech.delTech2(id);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n27_crtLeas_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int ID_user = 1;
            string address = "Адрес 3";
            string data_start = "2021-12-30";
            string data_fin = "2021-12-30";
            string summ = "100";

            //act
            result = DBF_Leas.crtLeas(ID_user, address, data_start, data_fin, summ);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n28_detailLeas_truereturn()
        {
            //arrage
            string[] expected = new string[] { "3", "Адрес 3", "100", "1 1" };
            string[] result;

            int id = 3;

            //act
            result = DBF_Leas.detail(id);

            //assert
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);
            Assert.AreEqual(expected[2], result[2]);
            Assert.AreEqual(expected[3], result[3]);
        }

        [TestMethod]
        public void n29_crtAttached_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            StatLeas.ID = 3;
            string ID_tech = "1";

            //act
            result = DBF_Leas.crtAttached(ID_tech);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n30_delAttached_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            StatLeas.ID = 3;
            int id = 1;

            //act
            result = DBF_Leas.delAttached(id);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void n31_delLeas_truereturn()
        {
            //arrage
            bool expected = true;
            bool result;

            int id = 3;

            //act
            result = DBF_Leas.delLeas(id);

            //assert
            Assert.AreEqual(expected, result);
        }
    }
}
