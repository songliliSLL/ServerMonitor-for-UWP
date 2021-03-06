﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerMonitor.Services.RequestServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestServerMonitor.TestRequest
{
    [TestClass]
    public class TestSMTPRequest     //SMTPR测试
    {
        private static string testDomainName = "smtp.163.com";
        private static int testport = 25;
        private SMTPRequest smtpRequest = new SMTPRequest(testDomainName, testport);

        [Owner("Bin")]
        [TestInitialize()] // 测试类生成预处理
        public void Initialize()
        {
            Debug.WriteLine("TestInitialize 方法调用");
        }

        [Owner("Bin")]
        [TestMethod]
        public void TestSMTPRequest_NormallySucceed()// 测试发起SMTP请求 
        {
            Assert.AreNotEqual(null, smtpRequest);
            Assert.AreEqual(true, smtpRequest.MakeRequest().Result, "Cannot get the right SMTP service");
            Assert.AreEqual("1000", smtpRequest.Status, "Cannot get the \'Succeed\' status code!");
        }

        /// <summary>
        /// 测试请求的域名为空
        /// </summary>
        [Owner("Bin")]
        [TestMethod]
        public void TestSMTPRequest_DomainNameValueNull()       //测试发起SMTP请求
        {
            smtpRequest.DomainName = "";
            Assert.AreNotEqual(null, smtpRequest);
            Assert.IsFalse(smtpRequest.MakeRequest().Result, " The DomainName is null");
            Assert.AreEqual("1001", smtpRequest.Status, "Cannot get the \'Error\' status code!");
        }
        /// <summary>
        /// 测试请求超时
        /// </summary>
        [Owner("Bin")]
        [TestMethod]
        public void TestSMTPRequest_OutTime()                 //测试发起SMTP请求
        {
            smtpRequest.OverTime = 1;
            Assert.AreNotEqual(null, smtpRequest);
            smtpRequest.MakeRequest().Wait();
            Assert.IsFalse(smtpRequest.MakeRequest().Result, "It is TimeOver!");
            Assert.AreEqual("1002", smtpRequest.Status, "Cannot get the \'OverTime\' status code!");
        }

        /// <summary>
        /// 测试请求的端口有误
        /// </summary>
        [Owner("Bin")]
        [TestMethod]
        public void TestSMTPRequest_NotTruePort()                   //测试发起SMTP请求
        {
            smtpRequest.Port = 465;
            Assert.AreNotEqual(null,smtpRequest);
            Assert.IsFalse(smtpRequest.MakeRequest().Result, "The Port is false");
            Assert.AreEqual("1001", smtpRequest.Status, "Cannot get the \'Error\' status code!");

        }

        /// <summary>
        /// 测试请求非法的域名
        /// </summary>
        [Owner("Bin")]
        [TestMethod]
        public void TestSMTPRequest_InvaildDomainName()              //测试发起SMTP请求
        {
            smtpRequest.DomainName = "smtp.16111113.com";
            smtpRequest.Port = 25;
            Assert.IsFalse(smtpRequest.MakeRequest().Result, "Cannot analysis the true DomainName");
            Assert.AreEqual("1001", smtpRequest.Status,"Cannot get the \'Error\' status code!");
        }
    }
}
