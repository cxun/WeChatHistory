using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.Threading;
using EO.WebBrowser;

namespace WechatHistory
{
    public partial class MainForm : Form
    {
        string m_strSqlFile;  // 数据库文件路径
        string m_strRoot;  // 聊天记录根目录
        int m_nMaxRecord = 20;  // 聊天记录每页最大记录数
        string m_strBody = "<body oncontextmenu=\"return false;\">\r\n%REPLACE%</body>\r\n";
        #region emoji css
        string m_strEmojiCSS =
            ".emoji { background: url(\"%EMOJIPATH%emoji17ced3.png\") top left no-repeat; width: 20px; height: 20px; display:inline; display: -moz-inline-stack; display: inline-block; vertical-align: top; zoom: 1; *display: inline; }\n" +
			".emoji2600 { background-position: 0px -260px; }\n" +
			".emoji2601 { background-position: 0px -20px; }\n" +
			".emoji2614 { background-position: 0px -40px; }\n" +
			".emoji26c4 { background-position: 0px -60px; }\n" +
			".emoji26a1 { background-position: 0px -80px; }\n" +
			".emoji1f300 { background-position: 0px -100px; }\n" +
			".emoji1f301 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f302 { background-position: 0px -120px; }\n" +
			".emoji1f303 { background-position: 0px -920px; }\n" +
			".emoji1f304 { background-position: 0px -160px; }\n" +
			".emoji1f305 { background-position: 0px -180px; }\n" +
			".emoji1f306 { background-position: 0px -200px; }\n" +
			".emoji1f307 { background-position: 0px -220px; }\n" +
			".emoji1f308 { background-position: 0px -240px; }\n" +
			".emoji2744 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji26c5 { background-position: 0px -260px; }\n" +
			".emoji1f309 { background-position: 0px -920px; }\n" +
			".emoji1f30a { background-position: 0px -900px; }\n" +
			".emoji1f30b { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f30c { background-position: 0px -920px; }\n" +
			".emoji1f30f { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f311 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f314 { background-position: 0px -360px; }\n" +
			".emoji1f313 { background-position: 0px -360px; }\n" +
			".emoji1f319 { background-position: 0px -360px; }\n" +
			".emoji1f315 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f31b { background-position: 0px -360px; }\n" +
			".emoji1f31f { background-position: 0px -10740px; }\n" +
			".emoji1f320 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f550 { background-position: 0px -380px; }\n" +
			".emoji1f551 { background-position: 0px -400px; }\n" +
			".emoji1f552 { background-position: 0px -420px; }\n" +
			".emoji1f553 { background-position: 0px -440px; }\n" +
			".emoji1f554 { background-position: 0px -460px; }\n" +
			".emoji1f555 { background-position: 0px -480px; }\n" +
			".emoji1f556 { background-position: 0px -500px; }\n" +
			".emoji1f557 { background-position: 0px -520px; }\n" +
			".emoji1f558 { background-position: 0px -540px; }\n" +
			".emoji1f559 { background-position: 0px -620px; }\n" +
			".emoji1f55a { background-position: 0px -580px; }\n" +
			".emoji1f55b { background-position: 0px -600px; }\n" +
			".emoji231a { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji231b { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji23f0 { background-position: 0px -620px; }\n" +
			".emoji23f3 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2648 { background-position: 0px -640px; }\n" +
			".emoji2649 { background-position: 0px -660px; }\n" +
			".emoji264a { background-position: 0px -680px; }\n" +
			".emoji264b { background-position: 0px -700px; }\n" +
			".emoji264c { background-position: 0px -720px; }\n" +
			".emoji264d { background-position: 0px -740px; }\n" +
			".emoji264e { background-position: 0px -760px; }\n" +
			".emoji264f { background-position: 0px -780px; }\n" +
			".emoji2650 { background-position: 0px -800px; }\n" +
			".emoji2651 { background-position: 0px -820px; }\n" +
			".emoji2652 { background-position: 0px -840px; }\n" +
			".emoji2653 { background-position: 0px -860px; }\n" +
			".emoji26ce { background-position: 0px -880px; }\n" +
			".emoji1f340 { background-position: 0px -1220px; }\n" +
			".emoji1f337 { background-position: 0px -960px; }\n" +
			".emoji1f331 { background-position: 0px -1220px; }\n" +
			".emoji1f341 { background-position: 0px -1000px; }\n" +
			".emoji1f338 { background-position: 0px -1020px; }\n" +
			".emoji1f339 { background-position: 0px -1040px; }\n" +
			".emoji1f342 { background-position: 0px -1060px; }\n" +
			".emoji1f343 { background-position: 0px -1080px; }\n" +
			".emoji1f33a { background-position: 0px -1100px; }\n" +
			".emoji1f33b { background-position: 0px -1200px; }\n" +
			".emoji1f334 { background-position: 0px -1140px; }\n" +
			".emoji1f335 { background-position: 0px -1160px; }\n" +
			".emoji1f33e { background-position: 0px -1180px; }\n" +
			".emoji1f33d { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f344 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f330 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f33c { background-position: 0px -1200px; }\n" +
			".emoji1f33f { background-position: 0px -1220px; }\n" +
			".emoji1f352 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f34c { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f34e { background-position: 0px -1360px; }\n" +
			".emoji1f34a { background-position: 0px -1260px; }\n" +
			".emoji1f353 { background-position: 0px -1280px; }\n" +
			".emoji1f349 { background-position: 0px -1300px; }\n" +
			".emoji1f345 { background-position: 0px -1320px; }\n" +
			".emoji1f346 { background-position: 0px -1340px; }\n" +
			".emoji1f348 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f34d { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f347 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f351 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f34f { background-position: 0px -1360px; }\n" +
			".emoji1f440 { background-position: 0px -1380px; }\n" +
			".emoji1f442 { background-position: 0px -1400px; }\n" +
			".emoji1f443 { background-position: 0px -1420px; }\n" +
			".emoji1f444 { background-position: 0px -1440px; }\n" +
			".emoji1f445 { background-position: 0px -3000px; }\n" +
			".emoji1f484 { background-position: 0px -1480px; }\n" +
			".emoji1f485 { background-position: 0px -1500px; }\n" +
			".emoji1f486 { background-position: 0px -1520px; }\n" +
			".emoji1f487 { background-position: 0px -1540px; }\n" +
			".emoji1f488 { background-position: 0px -1560px; }\n" +
			".emoji1f464 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f466 { background-position: 0px -1580px; }\n" +
			".emoji1f467 { background-position: 0px -1600px; }\n" +
			".emoji1f468 { background-position: 0px -1620px; }\n" +
			".emoji1f469 { background-position: 0px -1640px; }\n" +
			".emoji1f46a { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f46b { background-position: 0px -1660px; }\n" +
			".emoji1f46e { background-position: 0px -1680px; }\n" +
			".emoji1f46f { background-position: 0px -1700px; }\n" +
			".emoji1f470 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f471 { background-position: 0px -1720px; }\n" +
			".emoji1f472 { background-position: 0px -1740px; }\n" +
			".emoji1f473 { background-position: 0px -1760px; }\n" +
			".emoji1f474 { background-position: 0px -1780px; }\n" +
			".emoji1f475 { background-position: 0px -1800px; }\n" +
			".emoji1f476 { background-position: 0px -1820px; }\n" +
			".emoji1f477 { background-position: 0px -1840px; }\n" +
			".emoji1f478 { background-position: 0px -1860px; }\n" +
			".emoji1f479 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f47a { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f47b { background-position: 0px -1880px; }\n" +
			".emoji1f47c { background-position: 0px -1900px; }\n" +
			".emoji1f47d { background-position: 0px -1920px; }\n" +
			".emoji1f47e { background-position: 0px -1940px; }\n" +
			".emoji1f47f { background-position: 0px -1960px; }\n" +
			".emoji1f480 { background-position: 0px -1980px; }\n" +
			".emoji1f481 { background-position: 0px -2000px; }\n" +
			".emoji1f482 { background-position: 0px -2020px; }\n" +
			".emoji1f483 { background-position: 0px -2040px; }\n" +
			".emoji1f40c { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f40d { background-position: 0px -2620px; }\n" +
			".emoji1f40e { background-position: 0px -6680px; }\n" +
			".emoji1f414 { background-position: 0px -2640px; }\n" +
			".emoji1f417 { background-position: 0px -2660px; }\n" +
			".emoji1f42b { background-position: 0px -2680px; }\n" +
			".emoji1f418 { background-position: 0px -2480px; }\n" +
			".emoji1f428 { background-position: 0px -2500px; }\n" +
			".emoji1f412 { background-position: 0px -2520px; }\n" +
			".emoji1f411 { background-position: 0px -2540px; }\n" +
			".emoji1f419 { background-position: 0px -2340px; }\n" +
			".emoji1f41a { background-position: 0px -2360px; }\n" +
			".emoji1f41b { background-position: 0px -2460px; }\n" +
			".emoji1f41c { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f41d { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f41e { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f420 { background-position: 0px -2420px; }\n" +
			".emoji1f421 { background-position: 0px -7360px; }\n" +
			".emoji1f422 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f424 { background-position: 0px -2140px; }\n" +
			".emoji1f425 { background-position: 0px -2140px; }\n" +
			".emoji1f426 { background-position: 0px -2400px; }\n" +
			".emoji1f423 { background-position: 0px -2140px; }\n" +
			".emoji1f427 { background-position: 0px -2160px; }\n" +
			".emoji1f429 { background-position: 0px -2720px; }\n" +
			".emoji1f41f { background-position: 0px -7360px; }\n" +
			".emoji1f42c { background-position: 0px -2380px; }\n" +
			".emoji1f42d { background-position: 0px -2280px; }\n" +
			".emoji1f42f { background-position: 0px -2240px; }\n" +
			".emoji1f431 { background-position: 0px -2080px; }\n" +
			".emoji1f433 { background-position: 0px -2300px; }\n" +
			".emoji1f434 { background-position: 0px -2200px; }\n" +
			".emoji1f435 { background-position: 0px -2320px; }\n" +
			".emoji1f436 { background-position: 0px -2720px; }\n" +
			".emoji1f437 { background-position: 0px -2780px; }\n" +
			".emoji1f43b { background-position: 0px -2260px; }\n" +
			".emoji1f439 { background-position: 0px -2440px; }\n" +
			".emoji1f43a { background-position: 0px -2560px; }\n" +
			".emoji1f42e { background-position: 0px -2580px; }\n" +
			".emoji1f430 { background-position: 0px -2600px; }\n" +
			".emoji1f438 { background-position: 0px -2700px; }\n" +
			".emoji1f43e { background-position: 0px -6460px; }\n" +
			".emoji1f432 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f43c { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f43d { background-position: 0px -2780px; }\n" +
			".emoji1f620 { background-position: 0px -2800px; }\n" +
			".emoji1f629 { background-position: 0px -3860px; }\n" +
			".emoji1f632 { background-position: 0px -2840px; }\n" +
			".emoji1f61e { background-position: 0px -2860px; }\n" +
			".emoji1f635 { background-position: 0px -3540px; }\n" +
			".emoji1f630 { background-position: 0px -2900px; }\n" +
			".emoji1f612 { background-position: 0px -2920px; }\n" +
			".emoji1f60d { background-position: 0px -3660px; }\n" +
			".emoji1f624 { background-position: 0px -3720px; }\n" +
			".emoji1f61c { background-position: 0px -2980px; }\n" +
			".emoji1f61d { background-position: 0px -3000px; }\n" +
			".emoji1f60b { background-position: 0px -3220px; }\n" +
			".emoji1f618 { background-position: 0px -3640px; }\n" +
			".emoji1f61a { background-position: 0px -3060px; }\n" +
			".emoji1f637 { background-position: 0px -3080px; }\n" +
			".emoji1f633 { background-position: 0px -3100px; }\n" +
			".emoji1f603 { background-position: 0px -3580px; }\n" +
			".emoji1f605 { background-position: 0px -3260px; }\n" +
			".emoji1f606 { background-position: 0px -3380px; }\n" +
			".emoji1f601 { background-position: 0px -3720px; }\n" +
			".emoji1f602 { background-position: 0px -3620px; }\n" +
			".emoji1f60a { background-position: 0px -3220px; }\n" +
			".emoji263a { background-position: 0px -3240px; }\n" +
			".emoji1f604 { background-position: 0px -3260px; }\n" +
			".emoji1f622 { background-position: 0px -3680px; }\n" +
			".emoji1f62d { background-position: 0px -3300px; }\n" +
			".emoji1f628 { background-position: 0px -3320px; }\n" +
			".emoji1f623 { background-position: 0px -3540px; }\n" +
			".emoji1f621 { background-position: 0px -3880px; }\n" +
			".emoji1f60c { background-position: 0px -3380px; }\n" +
			".emoji1f616 { background-position: 0px -10520px; }\n" +
			".emoji1f614 { background-position: 0px -3860px; }\n" +
			".emoji1f631 { background-position: 0px -3440px; }\n" +
			".emoji1f62a { background-position: 0px -3460px; }\n" +
			".emoji1f60f { background-position: 0px -3480px; }\n" +
			".emoji1f613 { background-position: 0px -3500px; }\n" +
			".emoji1f625 { background-position: 0px -3520px; }\n" +
			".emoji1f62b { background-position: 0px -3540px; }\n" +
			".emoji1f609 { background-position: 0px -3560px; }\n" +
			".emoji1f63a { background-position: 0px -3580px; }\n" +
			".emoji1f638 { background-position: 0px -3720px; }\n" +
			".emoji1f639 { background-position: 0px -3620px; }\n" +
			".emoji1f63d { background-position: 0px -3640px; }\n" +
			".emoji1f63b { background-position: 0px -3660px; }\n" +
			".emoji1f63f { background-position: 0px -3680px; }\n" +
			".emoji1f63e { background-position: 0px -3880px; }\n" +
			".emoji1f63c { background-position: 0px -3720px; }\n" +
			".emoji1f640 { background-position: 0px -3860px; }\n" +
			".emoji1f645 { background-position: 0px -3760px; }\n" +
			".emoji1f646 { background-position: 0px -3780px; }\n" +
			".emoji1f647 { background-position: 0px -3800px; }\n" +
			".emoji1f648 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f64a { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f649 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f64b { background-position: 0px -11240px; }\n" +
			".emoji1f64c { background-position: 0px -3840px; }\n" +
			".emoji1f64d { background-position: 0px -3860px; }\n" +
			".emoji1f64e { background-position: 0px -3880px; }\n" +
			".emoji1f64f { background-position: 0px -3900px; }\n" +
			".emoji1f3e0 { background-position: 0px -3940px; }\n" +
			".emoji1f3e1 { background-position: 0px -3940px; }\n" +
			".emoji1f3e2 { background-position: 0px -3960px; }\n" +
			".emoji1f3e3 { background-position: 0px -3980px; }\n" +
			".emoji1f3e5 { background-position: 0px -4000px; }\n" +
			".emoji1f3e6 { background-position: 0px -4020px; }\n" +
			".emoji1f3e7 { background-position: 0px -4040px; }\n" +
			".emoji1f3e8 { background-position: 0px -4060px; }\n" +
			".emoji1f3e9 { background-position: 0px -4080px; }\n" +
			".emoji1f3ea { background-position: 0px -4100px; }\n" +
			".emoji1f3eb { background-position: 0px -4120px; }\n" +
			".emoji26ea { background-position: 0px -4140px; }\n" +
			".emoji26f2 { background-position: 0px -4160px; }\n" +
			".emoji1f3ec { background-position: 0px -4180px; }\n" +
			".emoji1f3ef { background-position: 0px -4200px; }\n" +
			".emoji1f3f0 { background-position: 0px -4220px; }\n" +
			".emoji1f3ed { background-position: 0px -4240px; }\n" +
			".emoji2693 { background-position: 0px -6920px; }\n" +
			".emoji1f3ee { background-position: 0px -8800px; }\n" +
			".emoji1f5fb { background-position: 0px -4300px; }\n" +
			".emoji1f5fc { background-position: 0px -4320px; }\n" +
			".emoji1f5fd { background-position: 0px -4340px; }\n" +
			".emoji1f5fe { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f5ff { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f45e { background-position: 0px -4400px; }\n" +
			".emoji1f45f { background-position: 0px -4400px; }\n" +
			".emoji1f460 { background-position: 0px -4420px; }\n" +
			".emoji1f461 { background-position: 0px -4440px; }\n" +
			".emoji1f462 { background-position: 0px -4460px; }\n" +
			".emoji1f463 { background-position: 0px -6460px; }\n" +
			".emoji1f453 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f455 { background-position: 0px -4620px; }\n" +
			".emoji1f456 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f451 { background-position: 0px -4500px; }\n" +
			".emoji1f454 { background-position: 0px -4520px; }\n" +
			".emoji1f452 { background-position: 0px -4540px; }\n" +
			".emoji1f457 { background-position: 0px -4560px; }\n" +
			".emoji1f458 { background-position: 0px -4580px; }\n" +
			".emoji1f459 { background-position: 0px -4600px; }\n" +
			".emoji1f45a { background-position: 0px -4620px; }\n" +
			".emoji1f45b { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f45c { background-position: 0px -4960px; }\n" +
			".emoji1f45d { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4b0 { background-position: 0px -4720px; }\n" +
			".emoji1f4b1 { background-position: 0px -4660px; }\n" +
			".emoji1f4b9 { background-position: 0px -6380px; }\n" +
			".emoji1f4b2 { background-position: 0px -4720px; }\n" +
			".emoji1f4b3 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4b4 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4b5 { background-position: 0px -4720px; }\n" +
			".emoji1f4b8 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f1e81f1f3 { background-position: 0px -4740px; }\n" +
			".emoji1f1e91f1ea { background-position: 0px -4760px; }\n" +
			".emoji1f1ea1f1f8 { background-position: 0px -4780px; }\n" +
			".emoji1f1eb1f1f7 { background-position: 0px -4800px; }\n" +
			".emoji1f1ec1f1e7 { background-position: 0px -4820px; }\n" +
			".emoji1f1ee1f1f9 { background-position: 0px -4840px; }\n" +
			".emoji1f1ef1f1f5 { background-position: 0px -4860px; }\n" +
			".emoji1f1f01f1f7 { background-position: 0px -4880px; }\n" +
			".emoji1f1f71f1fa { background-position: 0px -4900px; }\n" +
			".emoji1f1fa1f1f8 { background-position: 0px -4920px; }\n" +
			".emoji1f525 { background-position: 0px -5040px; }\n" +
			".emoji1f526 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f527 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f528 { background-position: 0px -4360px; }\n" +
			".emoji1f529 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f52a { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f52b { background-position: 0px -5020px; }\n" +
			".emoji1f52e { background-position: 0px -5080px; }\n" +
			".emoji1f52f { background-position: 0px -5080px; }\n" +
			".emoji1f530 { background-position: 0px -9660px; }\n" +
			".emoji1f531 { background-position: 0px -9680px; }\n" +
			".emoji1f489 { background-position: 0px -5300px; }\n" +
			".emoji1f48a { background-position: 0px -5320px; }\n" +
			".emoji1f170 { background-position: 0px -5340px; }\n" +
			".emoji1f171 { background-position: 0px -5360px; }\n" +
			".emoji1f18e { background-position: 0px -5380px; }\n" +
			".emoji1f17e { background-position: 0px -5400px; }\n" +
			".emoji1f380 { background-position: 0px -5420px; }\n" +
			".emoji1f381 { background-position: 0px -6080px; }\n" +
			".emoji1f382 { background-position: 0px -5460px; }\n" +
			".emoji1f384 { background-position: 0px -5480px; }\n" +
			".emoji1f385 { background-position: 0px -5500px; }\n" +
			".emoji1f38c { background-position: 0px -5520px; }\n" +
			".emoji1f386 { background-position: 0px -5540px; }\n" +
			".emoji1f388 { background-position: 0px -5560px; }\n" +
			".emoji1f389 { background-position: 0px -5580px; }\n" +
			".emoji1f38d { background-position: 0px -5600px; }\n" +
			".emoji1f38e { background-position: 0px -5620px; }\n" +
			".emoji1f393 { background-position: 0px -5640px; }\n" +
			".emoji1f392 { background-position: 0px -5660px; }\n" +
			".emoji1f38f { background-position: 0px -5680px; }\n" +
			".emoji1f387 { background-position: 0px -5700px; }\n" +
			".emoji1f390 { background-position: 0px -5720px; }\n" +
			".emoji1f383 { background-position: 0px -5740px; }\n" +
			".emoji1f38a { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f38b { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f391 { background-position: 0px -5760px; }\n" +
			".emoji1f4df { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji260e { background-position: 0px -5800px; }\n" +
			".emoji1f4de { background-position: 0px -5800px; }\n" +
			".emoji1f4f1 { background-position: 0px -5820px; }\n" +
			".emoji1f4f2 { background-position: 0px -5840px; }\n" +
			".emoji1f4dd { background-position: 0px -6440px; }\n" +
			".emoji1f4e0 { background-position: 0px -5880px; }\n" +
			".emoji2709 { background-position: 0px -11200px; }\n" +
			".emoji1f4e8 { background-position: 0px -11200px; }\n" +
			".emoji1f4e9 { background-position: 0px -11200px; }\n" +
			".emoji1f4ea { background-position: 0px -5980px; }\n" +
			".emoji1f4eb { background-position: 0px -5980px; }\n" +
			".emoji1f4ee { background-position: 0px -6000px; }\n" +
			".emoji1f4f0 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4e2 { background-position: 0px -6020px; }\n" +
			".emoji1f4e3 { background-position: 0px -6040px; }\n" +
			".emoji1f4e1 { background-position: 0px -6060px; }\n" +
			".emoji1f4e4 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4e5 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4e6 { background-position: 0px -6080px; }\n" +
			".emoji1f4e7 { background-position: 0px -11200px; }\n" +
			".emoji1f520 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f521 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f522 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f523 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f524 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2712 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4ba { background-position: 0px -6100px; }\n" +
			".emoji1f4bb { background-position: 0px -6120px; }\n" +
			".emoji270f { background-position: 0px -6440px; }\n" +
			".emoji1f4ce { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4bc { background-position: 0px -6160px; }\n" +
			".emoji1f4bd { background-position: 0px -6200px; }\n" +
			".emoji1f4be { background-position: 0px -6200px; }\n" +
			".emoji1f4bf { background-position: 0px -7800px; }\n" +
			".emoji1f4c0 { background-position: 0px -7820px; }\n" +
			".emoji2702 { background-position: 0px -6220px; }\n" +
			".emoji1f4cd { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4c3 { background-position: 0px -6440px; }\n" +
			".emoji1f4c4 { background-position: 0px -6440px; }\n" +
			".emoji1f4c5 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4c1 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4c2 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4d3 { background-position: 0px -6420px; }\n" +
			".emoji1f4d6 { background-position: 0px -6420px; }\n" +
			".emoji1f4d4 { background-position: 0px -6420px; }\n" +
			".emoji1f4d5 { background-position: 0px -6420px; }\n" +
			".emoji1f4d7 { background-position: 0px -6420px; }\n" +
			".emoji1f4d8 { background-position: 0px -6420px; }\n" +
			".emoji1f4d9 { background-position: 0px -6420px; }\n" +
			".emoji1f4da { background-position: 0px -6420px; }\n" +
			".emoji1f4db { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4dc { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4cb { background-position: 0px -6440px; }\n" +
			".emoji1f4c6 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4ca { background-position: 0px -6380px; }\n" +
			".emoji1f4c8 { background-position: 0px -6380px; }\n" +
			".emoji1f4c9 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4c7 { background-position: 0px -6420px; }\n" +
			".emoji1f4cc { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4d2 { background-position: 0px -6420px; }\n" +
			".emoji1f4cf { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4d0 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4d1 { background-position: 0px -6440px; }\n" +
			".emoji1f3bd { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji26be { background-position: 0px -6480px; }\n" +
			".emoji26f3 { background-position: 0px -6500px; }\n" +
			".emoji1f3be { background-position: 0px -6520px; }\n" +
			".emoji26bd { background-position: 0px -6540px; }\n" +
			".emoji1f3bf { background-position: 0px -6560px; }\n" +
			".emoji1f3c0 { background-position: 0px -6580px; }\n" +
			".emoji1f3c1 { background-position: 0px -6600px; }\n" +
			".emoji1f3c2 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f3c3 { background-position: 0px -6620px; }\n" +
			".emoji1f3c4 { background-position: 0px -6640px; }\n" +
			".emoji1f3c6 { background-position: 0px -6660px; }\n" +
			".emoji1f3c8 { background-position: 0px -6700px; }\n" +
			".emoji1f3ca { background-position: 0px -6720px; }\n" +
			".emoji1f683 { background-position: 0px -6740px; }\n" +
			".emoji1f687 { background-position: 0px -6780px; }\n" +
			".emoji24c2 { background-position: 0px -6780px; }\n" +
			".emoji1f684 { background-position: 0px -6800px; }\n" +
			".emoji1f685 { background-position: 0px -6820px; }\n" +
			".emoji1f697 { background-position: 0px -6840px; }\n" +
			".emoji1f699 { background-position: 0px -6860px; }\n" +
			".emoji1f68c { background-position: 0px -6880px; }\n" +
			".emoji1f68f { background-position: 0px -6900px; }\n" +
			".emoji1f6a2 { background-position: 0px -6920px; }\n" +
			".emoji2708 { background-position: 0px -6940px; }\n" +
			".emoji26f5 { background-position: 0px -6960px; }\n" +
			".emoji1f689 { background-position: 0px -7000px; }\n" +
			".emoji1f680 { background-position: 0px -7020px; }\n" +
			".emoji1f6a4 { background-position: 0px -7040px; }\n" +
			".emoji1f695 { background-position: 0px -7060px; }\n" +
			".emoji1f69a { background-position: 0px -7100px; }\n" +
			".emoji1f692 { background-position: 0px -7120px; }\n" +
			".emoji1f691 { background-position: 0px -7140px; }\n" +
			".emoji1f693 { background-position: 0px -7260px; }\n" +
			".emoji26fd { background-position: 0px -7180px; }\n" +
			".emoji1f17f { background-position: 0px -7200px; }\n" +
			".emoji1f6a5 { background-position: 0px -7220px; }\n" +
			".emoji1f6a7 { background-position: 0px -9720px; }\n" +
			".emoji1f6a8 { background-position: 0px -7260px; }\n" +
			".emoji2668 { background-position: 0px -7280px; }\n" +
			".emoji26fa { background-position: 0px -7300px; }\n" +
			".emoji1f3a0 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f3a1 { background-position: 0px -7320px; }\n" +
			".emoji1f3a2 { background-position: 0px -7340px; }\n" +
			".emoji1f3a3 { background-position: 0px -7360px; }\n" +
			".emoji1f3a4 { background-position: 0px -7380px; }\n" +
			".emoji1f3a5 { background-position: 0px -7400px; }\n" +
			".emoji1f3a6 { background-position: 0px -7420px; }\n" +
			".emoji1f3a7 { background-position: 0px -7440px; }\n" +
			".emoji1f3a8 { background-position: 0px -7460px; }\n" +
			".emoji1f3a9 { background-position: 0px -7540px; }\n" +
			".emoji1f3aa { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f3ab { background-position: 0px -7500px; }\n" +
			".emoji1f3ac { background-position: 0px -7520px; }\n" +
			".emoji1f3ad { background-position: 0px -7540px; }\n" +
			".emoji1f3ae { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f004 { background-position: 0px -7560px; }\n" +
			".emoji1f3af { background-position: 0px -7580px; }\n" +
			".emoji1f3b0 { background-position: 0px -7600px; }\n" +
			".emoji1f3b1 { background-position: 0px -7620px; }\n" +
			".emoji1f3b2 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f3b3 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f3b4 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f0cf { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f3b5 { background-position: 0px -7640px; }\n" +
			".emoji1f3b6 { background-position: 0px -7740px; }\n" +
			".emoji1f3b7 { background-position: 0px -7680px; }\n" +
			".emoji1f3b8 { background-position: 0px -7700px; }\n" +
			".emoji1f3b9 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f3ba { background-position: 0px -7720px; }\n" +
			".emoji1f3bb { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f3bc { background-position: 0px -7740px; }\n" +
			".emoji303d { background-position: 0px -7760px; }\n" +
			".emoji1f4f7 { background-position: 0px -4940px; }\n" +
			".emoji1f4f9 { background-position: 0px -7400px; }\n" +
			".emoji1f4fa { background-position: 0px -7780px; }\n" +
			".emoji1f4fb { background-position: 0px -7840px; }\n" +
			".emoji1f4fc { background-position: 0px -7860px; }\n" +
			".emoji1f48b { background-position: 0px -7900px; }\n" +
			".emoji1f48c { background-position: 0px -11200px; }\n" +
			".emoji1f48d { background-position: 0px -7940px; }\n" +
			".emoji1f48e { background-position: 0px -7960px; }\n" +
			".emoji1f48f { background-position: 0px -7980px; }\n" +
			".emoji1f490 { background-position: 0px -8000px; }\n" +
			".emoji1f491 { background-position: 0px -8020px; }\n" +
			".emoji1f492 { background-position: 0px -8040px; }\n" +
			".emoji1f51e { background-position: 0px -9860px; }\n" +
			".emojia9 { background-position: 0px -9900px; }\n" +
			".emojiae { background-position: 0px -9920px; }\n" +
			".emoji2122 { background-position: 0px -9940px; }\n" +
			".emoji2139 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2320e3 { background-position: 0px -8060px; }\n" +
			".emoji3120e3 { background-position: 0px -8080px; }\n" +
			".emoji3220e3 { background-position: 0px -8100px; }\n" +
			".emoji3320e3 { background-position: 0px -8120px; }\n" +
			".emoji3420e3 { background-position: 0px -8140px; }\n" +
			".emoji3520e3 { background-position: 0px -8160px; }\n" +
			".emoji3620e3 { background-position: 0px -8180px; }\n" +
			".emoji3720e3 { background-position: 0px -8200px; }\n" +
			".emoji3820e3 { background-position: 0px -8220px; }\n" +
			".emoji3920e3 { background-position: 0px -8240px; }\n" +
			".emoji3020e3 { background-position: 0px -8260px; }\n" +
			".emoji1f51f { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4f6 { background-position: 0px -8280px; }\n" +
			".emoji1f4f3 { background-position: 0px -8300px; }\n" +
			".emoji1f4f4 { background-position: 0px -8320px; }\n" +
			".emoji1f354 { background-position: 0px -8340px; }\n" +
			".emoji1f359 { background-position: 0px -8360px; }\n" +
			".emoji1f370 { background-position: 0px -8380px; }\n" +
			".emoji1f35c { background-position: 0px -8400px; }\n" +
			".emoji1f35e { background-position: 0px -8420px; }\n" +
			".emoji1f373 { background-position: 0px -8440px; }\n" +
			".emoji1f366 { background-position: 0px -8460px; }\n" +
			".emoji1f35f { background-position: 0px -8480px; }\n" +
			".emoji1f361 { background-position: 0px -8500px; }\n" +
			".emoji1f358 { background-position: 0px -8520px; }\n" +
			".emoji1f35a { background-position: 0px -8540px; }\n" +
			".emoji1f35d { background-position: 0px -8560px; }\n" +
			".emoji1f35b { background-position: 0px -8580px; }\n" +
			".emoji1f362 { background-position: 0px -8600px; }\n" +
			".emoji1f363 { background-position: 0px -8620px; }\n" +
			".emoji1f371 { background-position: 0px -8640px; }\n" +
			".emoji1f372 { background-position: 0px -8660px; }\n" +
			".emoji1f367 { background-position: 0px -8680px; }\n" +
			".emoji1f356 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f365 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f360 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f355 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f357 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f368 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f369 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f36a { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f36b { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f36c { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f36d { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f36e { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f36f { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f364 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f374 { background-position: 0px -8700px; }\n" +
			".emoji2615 { background-position: 0px -8720px; }\n" +
			".emoji1f378 { background-position: 0px -8860px; }\n" +
			".emoji1f37a { background-position: 0px -8760px; }\n" +
			".emoji1f375 { background-position: 0px -8780px; }\n" +
			".emoji1f376 { background-position: 0px -8800px; }\n" +
			".emoji1f377 { background-position: 0px -8860px; }\n" +
			".emoji1f37b { background-position: 0px -8840px; }\n" +
			".emoji1f379 { background-position: 0px -8860px; }\n" +
			".emoji2197 { background-position: 0px -8960px; }\n" +
			".emoji2198 { background-position: 0px -8980px; }\n" +
			".emoji2196 { background-position: 0px -8920px; }\n" +
			".emoji2199 { background-position: 0px -8940px; }\n" +
			".emoji2934 { background-position: 0px -8960px; }\n" +
			".emoji2935 { background-position: 0px -8980px; }\n" +
			".emoji2194 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2195 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2b06 { background-position: 0px -9000px; }\n" +
			".emoji2b07 { background-position: 0px -9020px; }\n" +
			".emoji27a1 { background-position: 0px -9040px; }\n" +
			".emoji2b05 { background-position: 0px -11180px; }\n" +
			".emoji25b6 { background-position: 0px -9080px; }\n" +
			".emoji25c0 { background-position: 0px -9100px; }\n" +
			".emoji23e9 { background-position: 0px -9120px; }\n" +
			".emoji23ea { background-position: 0px -9140px; }\n" +
			".emoji23eb { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji23ec { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f53a { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f53b { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f53c { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f53d { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2b55 { background-position: 0px -9800px; }\n" +
			".emoji274c { background-position: 0px -10340px; }\n" +
			".emoji274e { background-position: 0px -10340px; }\n" +
			".emoji2757 { background-position: 0px -9160px; }\n" +
			".emoji2049 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji203c { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2753 { background-position: 0px -9180px; }\n" +
			".emoji2754 { background-position: 0px -9200px; }\n" +
			".emoji2755 { background-position: 0px -9220px; }\n" +
			".emoji3030 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji27b0 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji27bf { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2764 { background-position: 0px -9240px; }\n" +
			".emoji1f493 { background-position: 0px -9480px; }\n" +
			".emoji1f494 { background-position: 0px -9280px; }\n" +
			".emoji1f495 { background-position: 0px -9480px; }\n" +
			".emoji1f496 { background-position: 0px -9480px; }\n" +
			".emoji1f497 { background-position: 0px -9340px; }\n" +
			".emoji1f498 { background-position: 0px -9360px; }\n" +
			".emoji1f499 { background-position: 0px -9380px; }\n" +
			".emoji1f49a { background-position: 0px -9400px; }\n" +
			".emoji1f49b { background-position: 0px -9420px; }\n" +
			".emoji1f49c { background-position: 0px -9440px; }\n" +
			".emoji1f49d { background-position: 0px -9460px; }\n" +
			".emoji1f49e { background-position: 0px -9480px; }\n" +
			".emoji1f49f { background-position: 0px -9500px; }\n" +
			".emoji2665 { background-position: 0px -9520px; }\n" +
			".emoji2660 { background-position: 0px -9540px; }\n" +
			".emoji2666 { background-position: 0px -9560px; }\n" +
			".emoji2663 { background-position: 0px -9580px; }\n" +
			".emoji1f6ac { background-position: 0px -9600px; }\n" +
			".emoji1f6ad { background-position: 0px -9620px; }\n" +
			".emoji267f { background-position: 0px -9640px; }\n" +
			".emoji1f6a9 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji26a0 { background-position: 0px -9700px; }\n" +
			".emoji26d4 { background-position: 0px -9720px; }\n" +
			".emoji267b { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f6b2 { background-position: 0px -6980px; }\n" +
			".emoji1f6b6 { background-position: 0px -7080px; }\n" +
			".emoji1f6b9 { background-position: 0px -9740px; }\n" +
			".emoji1f6ba { background-position: 0px -9760px; }\n" +
			".emoji1f6c0 { background-position: 0px -5220px; }\n" +
			".emoji1f6bb { background-position: 0px -5240px; }\n" +
			".emoji1f6bd { background-position: 0px -5260px; }\n" +
			".emoji1f6be { background-position: 0px -5280px; }\n" +
			".emoji1f6bc { background-position: 0px -9780px; }\n" +
			".emoji1f6aa { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f6ab { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2714 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f191 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f192 { background-position: 0px -10020px; }\n" +
			".emoji1f193 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f194 { background-position: 0px -11020px; }\n" +
			".emoji1f195 { background-position: 0px -9980px; }\n" +
			".emoji1f196 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f197 { background-position: 0px -9880px; }\n" +
			".emoji1f198 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f199 { background-position: 0px -10000px; }\n" +
			".emoji1f19a { background-position: 0px -9960px; }\n" +
			".emoji1f201 { background-position: 0px -10060px; }\n" +
			".emoji1f202 { background-position: 0px -10080px; }\n" +
			".emoji1f232 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f233 { background-position: 0px -10100px; }\n" +
			".emoji1f234 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f235 { background-position: 0px -10120px; }\n" +
			".emoji1f236 { background-position: 0px -10140px; }\n" +
			".emoji1f21a { background-position: 0px -10160px; }\n" +
			".emoji1f237 { background-position: 0px -10180px; }\n" +
			".emoji1f238 { background-position: 0px -10200px; }\n" +
			".emoji1f239 { background-position: 0px -10220px; }\n" +
			".emoji1f22f { background-position: 0px -10240px; }\n" +
			".emoji1f23a { background-position: 0px -10260px; }\n" +
			".emoji3299 { background-position: 0px -10280px; }\n" +
			".emoji3297 { background-position: 0px -10300px; }\n" +
			".emoji1f250 { background-position: 0px -10320px; }\n" +
			".emoji1f251 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2795 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2796 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2716 { background-position: 0px -10340px; }\n" +
			".emoji2797 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4a0 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4a1 { background-position: 0px -10360px; }\n" +
			".emoji1f4a2 { background-position: 0px -10380px; }\n" +
			".emoji1f4a3 { background-position: 0px -10400px; }\n" +
			".emoji1f4a4 { background-position: 0px -10420px; }\n" +
			".emoji1f4a5 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4a6 { background-position: 0px -10460px; }\n" +
			".emoji1f4a7 { background-position: 0px -10460px; }\n" +
			".emoji1f4a8 { background-position: 0px -10480px; }\n" +
			".emoji1f4a9 { background-position: 0px -5000px; }\n" +
			".emoji1f4aa { background-position: 0px -10500px; }\n" +
			".emoji1f4ab { background-position: 0px -10520px; }\n" +
			".emoji1f4ac { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2728 { background-position: 0px -11000px; }\n" +
			".emoji2734 { background-position: 0px -10560px; }\n" +
			".emoji2733 { background-position: 0px -10580px; }\n" +
			".emoji26aa { background-position: 0px -10640px; }\n" +
			".emoji26ab { background-position: 0px -10640px; }\n" +
			".emoji1f534 { background-position: 0px -10640px; }\n" +
			".emoji1f535 { background-position: 0px -10900px; }\n" +
			".emoji1f532 { background-position: 0px -10900px; }\n" +
			".emoji1f533 { background-position: 0px -10980px; }\n" +
			".emoji2b50 { background-position: 0px -10720px; }\n" +
			".emoji2b1c { background-position: 0px -10980px; }\n" +
			".emoji2b1b { background-position: 0px -10900px; }\n" +
			".emoji25ab { background-position: 0px -10980px; }\n" +
			".emoji25aa { background-position: 0px -10900px; }\n" +
			".emoji25fd { background-position: 0px -10980px; }\n" +
			".emoji25fe { background-position: 0px -10900px; }\n" +
			".emoji25fb { background-position: 0px -10980px; }\n" +
			".emoji25fc { background-position: 0px -10900px; }\n" +
			".emoji1f536 { background-position: 0px -10980px; }\n" +
			".emoji1f537 { background-position: 0px -10980px; }\n" +
			".emoji1f538 { background-position: 0px -10980px; }\n" +
			".emoji1f539 { background-position: 0px -10980px; }\n" +
			".emoji2747 { background-position: 0px -11000px; }\n" +
			".emoji1f4ae { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f4af { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji21a9 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji21aa { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f503 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f50a { background-position: 0px -7880px; }\n" +
			".emoji1f50b { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f50c { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f50d { background-position: 0px -11060px; }\n" +
			".emoji1f50e { background-position: 0px -11060px; }\n" +
			".emoji1f512 { background-position: 0px -11140px; }\n" +
			".emoji1f513 { background-position: 0px -11100px; }\n" +
			".emoji1f50f { background-position: 0px -11140px; }\n" +
			".emoji1f510 { background-position: 0px -11140px; }\n" +
			".emoji1f511 { background-position: 0px -11160px; }\n" +
			".emoji1f514 { background-position: 0px -4980px; }\n" +
			".emoji2611 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f518 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f516 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f517 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f519 { background-position: 0px -11180px; }\n" +
			".emoji1f51a { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f51b { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f51c { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji1f51d { background-position: 0px -10040px; }\n" +
			".emoji2003 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2002 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2005 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji2705 { background-position: 0px -9200px; } /* placeholder */\n" +
			".emoji270a { background-position: 0px -11220px; }\n" +
			".emoji270b { background-position: 0px -11240px; }\n" +
			".emoji270c { background-position: 0px -11260px; }\n" +
			".emoji1f44a { background-position: 0px -11280px; }\n" +
			".emoji1f44d { background-position: 0px -11300px; }\n" +
			".emoji261d { background-position: 0px -11320px; }\n" +
			".emoji1f446 { background-position: 0px -11340px; }\n" +
			".emoji1f447 { background-position: 0px -11360px; }\n" +
			".emoji1f448 { background-position: 0px -11380px; }\n" +
			".emoji1f449 { background-position: 0px -11400px; }\n" +
			".emoji1f44b { background-position: 0px -11420px; }\n" +
			".emoji1f44f { background-position: 0px -11440px; }\n" +
			".emoji1f44c { background-position: 0px -11460px; }\n" +
			".emoji1f44e { background-position: 0px -11480px; }\n" +
			".emoji1f450 { background-position: 0px -11500px; }\n";
        #endregion
        string m_strStyle = "<style>\r\ndiv.speech {\r\n margin: 10px 0;\r\n padding: 8px;\r\n table-layout: fixed;\r\n word-break: keep-all;\r\n position: relative;\r\n background: -webkit-gradient( linear, 50% 0%, 50% 100%, from(#ffffff), color-stop(0.1, #ececec), color-stop(0.5, #dbdbdb), color-stop(0.9, #dcdcdc), to(#8c8c8c) );\r\n border: 1px solid #989898;\r\n -webkit-border-radius: 8px;\r\n -moz-border-radius: 8px;\r\n border-radius: 8px;\r\n}\r\ndiv.speech:before {\r\n content: \'\';\r\n position: absolute;\r\n width: 0;\r\n height: 0;\r\n left: 15px;\r\n top: -20px;\r\n border: 10px solid;\r\n border-color: transparent transparent #989898 transparent;\r\n}\r\ndiv.speech:after {\r\n content: \'\';\r\n position: absolute;\r\n width: 0;\r\n height: 0;\r\n left: 17px;\r\n top: -16px;\r\n border: 8px solid;\r\n border-color: transparent transparent #ffffff transparent;\r\n}\r\ndiv.speech.right {\r\n box-shadow: -2px 2px 5px #CCC;\r\n margin-right: 10px;\r\n width: 100%;\r\n float: right;\r\n background: -webkit-gradient( linear, 50% 0%, 50% 100%, from(#e4ffa7), color-stop(0.1, #bced50), color-stop(0.4, #aed943), color-stop(0.8, #a7d143), to(#99BF40) );\r\n}\r\ndiv.speech.right:before {\r\n content: \'\';\r\n position: absolute;\r\n width: 0;\r\n height: 0;\r\n top: 9px;\r\n bottom: auto;\r\n left: auto;\r\n right: -10px;\r\n border-width: 9px 0 9px 10px;\r\n border-color: transparent #989898;\r\n}\r\ndiv.speech.right:after {\r\n content: \'\';\r\n position: absolute;\r\n width: 0;\r\n height: 0;\r\n top: 10px;\r\n bottom: auto;\r\n left: auto;\r\n right: -8px;\r\n border-width: 8px 0 8px 9px;\r\n border-color: transparent #bced50;\r\n}\r\ndiv.speech.left {\r\n box-shadow: 2px 2px 2px #CCCCCC;\r\n margin-left: 10px;\r\n width: 100%;\r\n float: left;\r\n background: -webkit-gradient( linear, 50% 0%, 50% 100%, from(#ffffff), color-stop(0.1, #eae8e8), color-stop(0.4, #E3E3E3), color-stop(0.8, #DFDFDF), to(#D9D9D9) );\r\n}\r\ndiv.speech.left:before {\r\n content: \'\';\r\n position: absolute;\r\n width: 0;\r\n height: 0;\r\n top: 9px;\r\n bottom: auto;\r\n left: -10px;\r\n border-width: 9px 10px 9px 0;\r\n border-color: transparent #989898;\r\n}\r\ndiv.speech.left:after {\r\n content: \'\';\r\n position: absolute;\r\n width: 0;\r\n height: 0;\r\n top: 10px;\r\n bottom: auto;\r\n left: -8px;\r\n border-width: 8px 9px 8px 0;\r\n border-color: transparent #eae8e8;\r\n}\r\n.leftimg {\r\n float: left;\r\n margin-top: 10px;\r\nfont-family: Verdana, Arial, Helvetica, sans-serif;\r\nfont-size: 12px;\r\ncolor: #cccccc;\r\n}\r\n.rightimg {\r\n float: right;\r\n margin-top: 10px;\r\nfont-family: Verdana, Arial, Helvetica, sans-serif;\r\nfont-size: 12px;\r\ncolor: #cccccc;\r\n}\r\n.leftd {\r\n clear: both;\r\n float: left;\r\n padding-left: 40px;\r\npadding-right: 100px;\r\nfont-family: 微软雅黑, 宋体, Verdana, Arial, Helvetica, sans-serif;\r\nfont-size: 16px;\r\n}\r\n.rightd {\r\n clear: both;\r\n float: right;\r\n padding-right: 40px;\r\npadding-left: 100px;\r\nfont-family: 微软雅黑, 宋体, Verdana, Arial, Helvetica, sans-serif;\r\nfont-size: 16px;\r\n}\r\n.clear {\r\n clear: both;\r\n}\r\n.block {\r\n    position: absolute;\r\n    width: 100%;\r\n    height: 100%;\r\n		filter:alpha(Opacity=90);\r\n		-moz-opacity:0.9;\r\n		opacity: 0.9;\r\n		cursor:hand;\r\n}\r\n.play-button:hover{\r\n        -webkit-box-shadow:rgba(255,255,255,0.8) 0 0 30px;\r\n        -moz-box-shadow:rgba(255,255,255,0.8) 0 0 30px;\r\n        box-shadow:rgba(255,255,255,0.8) 0 0 30px;        \r\n}\r\n.play-button {\r\n    width: 20px;\r\n    height: 20px;\r\n    position: absolute;\r\n    border: 2px solid #fff;\r\n    background: rgba(0, 0, 0, 0.8);\r\n    border-radius: 54px;\r\n    right:50%;\r\n    bottom:50%;\r\n}\r\n.play-button span {\r\n    position: absolute;\r\n    top: 3px;\r\n    left: 6px;\r\n    width: 0;\r\n    height: 0;\r\n    border-top: 7px solid transparent;\r\n    border-bottom: 7px solid transparent;\r\n    border-left: 12px solid #fff;\r\n}\r\ndiv.speech.left.audio {\r\n box-shadow: 2px 2px 2px #CCCCCC;\r\n margin-left: 10px;\r\n width: 100%;\r\n float: left;\r\n background: -webkit-gradient( linear, 50% 0%, 50% 100%, from(#ffffff), color-stop(0.1, #eae8e8), color-stop(0.4, #E3E3E3), color-stop(0.8, #DFDFDF), to(#D9D9D9) );\r\n width: 70px;\r\n cursor:hand;\r\n}\r\ndiv.speech.right.audio {\r\n box-shadow: -2px 2px 5px #CCC;\r\n margin-right: 10px;\r\n width: 100%;\r\n float: right;\r\n background: -webkit-gradient( linear, 50% 0%, 50% 100%, from(#e4ffa7), color-stop(0.1, #bced50), color-stop(0.4, #aed943), color-stop(0.8, #a7d143), to(#99BF40) );\r\n width: 70px;\r\n cursor:hand;\r\n}\r\n.audio_len.left\r\n{\r\n	float:right;\r\n	position:relative;\r\n	padding-top: 25px;\r\n	padding-left: 10px;\r\n	font-size: 12px;\r\n	color: #aaa;\r\n}\r\n.audio_len.right\r\n{\r\n	float:left;\r\n	position:relative;\r\n	padding-top: 25px;\r\n	padding-right: 10px;\r\n	font-size: 12px;\r\n	color: #aaa;\r\n}\r\n.audio_icon.right {\r\n    width: 20px;\r\n    height: 20px;\r\n    position: relative;\r\n    border-left: 1px solid #000;\r\n    border-radius: 54px;\r\n    float:right;\r\n}\r\n.sound1.left{\r\n	position:absolute;\r\n	width:60%;\r\n	height:60%;\r\n	top:20%;\r\n	left:-30%;\r\n	border-right:solid 2px;\r\n	border-color:rgba(0,0,0,0.5);\r\n	-webkit-border-radius:50%;\r\n	-moz-border-radius:0px 400px 400px 0px;\r\n}\r\n.sound2.left{\r\n	position:absolute;\r\n	width:80%;\r\n	height:80%;\r\n	top:10%;\r\n	left:-20%;\r\n	border-right:solid 2px;\r\n	border-color:rgba(0,0,0,0.5);\r\n	-webkit-border-radius:50%;\r\n	-moz-border-radius:0px 400px 400px 0px;\r\n}\r\n.sound3.left{\r\n	position:absolute;\r\n	width:100%;\r\n	height:100%;\r\n	top:0%;\r\n	left:-10%;\r\n	border-right:solid 2px;\r\n	border-color:rgba(0,0,0,0.5);\r\n	-webkit-border-radius:50%;\r\n	-moz-border-radius:0px 400px 400px 0px;\r\n}\r\n.sound_block.left\r\n{\r\n	position:relative;\r\n	float:left;\r\n	width:20px;\r\n	height:20px;\r\n	top:20px;\r\n	left:40px;\r\n	z-index:3;\r\n	cursor:hand;\r\n}\r\n.sound1.right{\r\n	position:absolute;\r\n	width:60%;\r\n	height:60%;\r\n	top:20%;\r\n	left:0%;\r\n	border-left:solid 2px;\r\n	border-color:rgba(0,128,0,0.7);\r\n	-webkit-border-radius:50%;\r\n	-moz-border-radius:0px 400px 400px 0px;\r\n}\r\n.sound2.right{\r\n	position:absolute;\r\n	width:80%;\r\n	height:80%;\r\n	top:10%;\r\n	left:-30%;\r\n	border-left:solid 2px;\r\n	border-color:rgba(0,128,0,0.7);\r\n	-webkit-border-radius:50%;\r\n	-moz-border-radius:0px 400px 400px 0px;\r\n}\r\n.sound3.right{\r\n	position:absolute;\r\n	width:100%;\r\n	height:100%;\r\n	top:0%;\r\n	left:-60%;\r\n	border-left:solid 2px;\r\n	border-color:rgba(0,128,0,0.7);\r\n	-webkit-border-radius:50%;\r\n	-moz-border-radius:0px 400px 400px 0px;\r\n}\r\n.sound_block.right\r\n{\r\n	position:relative;\r\n	float:right;\r\n	width:20px;\r\n	height:20px;\r\n	top:20px;\r\n	right:30px;\r\n	z-index:3;\r\n	cursor:hand;\r\n}\r\n%EMOJI_CSS%</style>\r\n";
        string m_strDivL = "\t<div class=\"leftd\">\r\n\t\t%REPLACE%\r\n\t</div>\r\n";
        string m_strDivR = "\t<div class=\"rightd\">\r\n\t\t%REPLACE%\r\n\t</div>\r\n";
        string m_strDivImgL = "<div class=\"leftimg\">\r\n\t\t\t%REPLACE%\r\n\t\t</div>\r\n";
        string m_strDivImgR = "<div class=\"rightimg\">\r\n\t\t\t%REPLACE%\r\n\t\t</div>\r\n";
        string m_strSpeechL = "<div class=\"speech left\">\r\n\t\t\t%REPLACE%\r\n\t\t</div>";
        string m_strSpeechR = "<div class=\"speech right\">\r\n\t\t\t%REPLACE%\r\n\t\t</div>";
        string m_strPlayBtn = "<div class=\"block\" name=\"%REPLACE%\"><span class=\"play-button\" name=\"%REPLACE%\"><span name=\"%REPLACE%\"></span></span></div>\r\n";
        string m_strAudioLenL = "\t\t<div class=\"audio_len left\">%REPLACE%</div>\r\n";
        string m_strAudioLenR = "\t\t<div class=\"audio_len right\">%REPLACE%</div>\r\n";
        string m_strAudioIconL = "\t\t<div class=\"sound_block left\" name=\"%REPLACE_NAME%\">\r\n\t\t\t<div id=\"sound1\" class=\"sound1 left\"></div>\r\n\t\t\t<div id=\"sound2\" class=\"sound2 left\"></div>\r\n\t\t\t<div id=\"sound3\" class=\"sound3 left\"></div>\r\n\t\t</div>\r\n";
        string m_strAudioIconR = "\t\t<div class=\"sound_block right\" name=\"%REPLACE_NAME%\">\r\n\t\t\t<div id=\"sound1\" class=\"sound1 right\"></div>\r\n\t\t\t<div id=\"sound2\" class=\"sound2 right\"></div>\r\n\t\t\t<div id=\"sound3\" class=\"sound3 right\"></div>\r\n\t\t</div>\r\n";
        string m_strSpeechAudioL = "\t\t<div class=\"speech left audio\" style=\"width:%REPLACE_WIDTH%px\" name=\"%REPLACE_NAME%\">&nbsp;&nbsp;&nbsp;&nbsp;</div>\r\n";
        string m_strSpeechAudioR = "\t\t<div class=\"speech right audio\" style=\"width:%REPLACE_WIDTH%px\" name=\"%REPLACE_NAME%\">&nbsp;&nbsp;&nbsp;&nbsp;</div>\r\n";

        int m_nMaxPage = 0;  // 当前好友聊天记录的最大页数
        int m_nCurPage = 0;  // 聊天记录的当前显示页数
        string m_strTmpPath = "";  // 记录临时文件夹，用以存放聊天记录文件
        int m_nLastClickTab = 0;   // 记录最后一次显示聊天记录时，所在的TAB
        Dictionary<string, FriendInfo> m_FriendList = new Dictionary<string, FriendInfo>();  // 存储所有好友的信息
        Dictionary<string, string> m_FriendPareList = new Dictionary<string, string>();  // 存储所有好友的信息（微信ID、显示名称）
        string[] m_strSelectedNodeArr = new string[3];  // 记录每一个tab页选择的是哪一个结点
        FriendSearchResultsForm m_fmFriendSearchResult = new FriendSearchResultsForm();
        string m_strLastFriend = "";  // 搜索聊天记录时，消息列表中点击的好友若与上次点击的好友一致，则不再重新加载整个聊天记录
        string m_strEmojiPath = "";  // 存放 emoji 的路径
        bool m_bShouldExit = false;  // 若为 TRUE， MainForm Shown 时将退出程序，实现启动时点击取消退出程序

        public MainForm()
        {
            InitializeComponent();
            m_strStyle = m_strStyle.Replace("%EMOJI_CSS%", m_strEmojiCSS);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            splitContainer1_Panel1_Resize(sender, e);
            splitContainer1_Panel2_Resize(sender, e);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            MainForm_Resize(sender, e);
            cbSearchArea.SelectedIndex = 0;
            tbSearchFriend_Leave(null, null);
            tbSearchHistory.Text = "搜索聊天记录";
            tbSearchHistory.ForeColor = Color.LightGray;
            this.TopMost = true;
            this.TopMost = false;
        }

        /// <summary>
        /// 从包含全拼的字符串中提取拼音
        /// </summary>
        /// <param name="strSrc">包含全拼的字符串</param>
        /// <param name="strPinyin">返回的全拼</param>
        private void GetPinyinFromString(string strSrc, out string strPinyin)
        {
            char[] arr = new char[26];
            for (int i = 0; i < 26; i++)
                arr[i] = (char)(0x61 + i);
            int nPos = 0;
            bool bFound = false;
            for (int i = 0; i < strSrc.Length; i++)
            {
                if (bFound) break;
                foreach (char cArr in arr)
                {
                    if (strSrc[i] == cArr)
                    {
                        nPos = i;
                        bFound = true;
                        break;
                    }
                }
            }
            if (nPos != 0)
            {
                int nEnd = strSrc.IndexOf((char)0x18, nPos);
                if (nEnd != -1)
                {
                    int length = nEnd - nPos;  // 拼音结尾
                    strPinyin = strSrc.Substring(nPos, length);
                }
                else
                    strPinyin = strSrc.Substring(nPos);
            }
            else
                strPinyin = "";
        }

        /// <summary>
        /// 加载好友列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "MM.sqlite|MM.sqlite";
            DialogResult dlgRes = dlg.ShowDialog();
            if (dlgRes == System.Windows.Forms.DialogResult.Cancel)
            {
                m_bShouldExit = true;
                return;
            }
            m_strSqlFile = dlg.FileName;
            try
            {
                m_strRoot = Directory.GetParent(m_strSqlFile).Parent.FullName + "\\";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("聊天记录文件夹结构不完整，详细信息：\r\n" + ex.Message);
                return;
            }

            SQLiteConnection connection = new SQLiteConnection("Data Source=" + m_strSqlFile);
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(connection);
            // 获取好友个数
            cmd.CommandText = "select count(*) from friend where type != 4 and type != 6";
            cmd.ExecuteNonQuery();
            SQLiteDataReader dr = cmd.ExecuteReader();
            int nTotal = 0;
            if (dr.Read())
                nTotal = dr.GetInt32(0);
            else
            {
                MessageBox.Show("读取好友列表时出现异常！");
                return;
            }
            dr.Close();
            // 开始获取好友信息
            cmd.CommandText = "select * from friend where type != 4 and type != 6";
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();

            TreeView[] tvArr = new TreeView[3];
            tvArr[0] = tvFriends;
            tvArr[1] = tvGroup;
            tvArr[2] = tvOthers;

            ProgressForm progressForm = new ProgressForm();
            progressForm.progressBar.Minimum = 0;
            progressForm.progressBar.Maximum = nTotal;
            progressForm.progressBar.Step = 1;
            progressForm.Show();

            while (dr.Read())
            {
                progressForm.progressBar.PerformStep();

                FriendInfo info = new FriendInfo();
                info.strUsrName = dr.GetString(1);
                info.strNickName = dr.GetString(2);
                info.strRemarkName = dr.GetString(8);
                info.strPinyin = dr.GetString(8);
                int nType = dr.GetInt16(10);
                string strFullPY = "";
                try
                {
                    strFullPY = dr.GetString(7);
                }
                catch (Exception ex) { }
                //info.strNickName = Microsoft.VisualBasic.Strings.StrConv(info.strNickName,
                //    Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0);
                if (info.strRemarkName.Substring(0, 2).CompareTo("\n\0") != 0)
                {
                    // 获取备注名
                    int length = info.strRemarkName.IndexOf((char)0x12) - 2;
                    if (length > 0)
                        info.strRemarkName = info.strRemarkName.Substring(2, length);
                    // 获取备注名的全拼
                    GetPinyinFromString(info.strPinyin, out info.strPinyin);
                    if (info.strPinyin.Length == 0)
                        GetPinyinFromString(strFullPY, out info.strPinyin);
                    if (info.strPinyin.Length == 0)
                        info.strPinyin = SpellCodeHelper.GetAllPYLetters(info.strNickName);
                }
                else
                {
                    info.strRemarkName = "";
                    info.strPinyin = "";
                }
                if (info.strPinyin.Length == 0)
                    info.strPinyin = SpellCodeHelper.GetAllPYLetters(info.strNickName);

                #region 输出查看
                //FileStream fs = new FileStream("a.txt", FileMode.OpenOrCreate | FileMode.Append);
                //string strFS = string.Format("{0}\t{1}\t{2}\t{3}\r\n", info.strUsrName, info.strNickName, info.strRemarkName, info.strPinyin);
                //byte[] bt = Encoding.UTF8.GetBytes(strFS);
                //fs.Write(bt, 0, bt.Length);
                //fs.Close();
                #endregion

                MD5 md5Hash = MD5.Create();
                string table = "Chat_" + GetMd5Hash(md5Hash, info.strUsrName);
                SQLiteCommand cmd2 = new SQLiteCommand(connection);
                cmd2.CommandText = "select * from " + table;
                try
                {
                    cmd2.ExecuteNonQuery();
                }
                catch (System.Data.SQLite.SQLiteException ex)
                {
                    //if (ex.Message.Contains("no such table"))
                    continue;
                }

                TreeNode node = new TreeNode();
                if (info.strRemarkName.Length == 0)
                    node.Text = info.strNickName;
                else
                    node.Text = info.strRemarkName;
                if (info.strRemarkName.Contains("<2></2>"))
                    node.Text = info.strNickName;
                if (node.Text.Length == 0)
                    node.Text = "未命名聊天群";
                //node.Text = info.strNickName + "【" + info.strRemarkName + "】";
                node.Tag = info;

                // 存储好友信息
                info.strDisplayName = node.Text;
                m_FriendList.Add(info.strUsrName, info);

                //分类
                string strPy = "#";
                char[] arr = new char[26];
                for (int i = 0; i < 26; i++)
                    arr[i] = (char)(0x61 + i);
                foreach (char c in info.strPinyin.ToLower())
                {
                    bool bFound = false;
                    foreach (char alphabet in arr)
                    {
                        if (c == alphabet)
                        {
                            strPy = c + "";
                            bFound = true;
                            break;
                        }
                    }
                    if (bFound) break;
                }
                strPy = strPy.ToUpper();
                //if (info.strPinyin.Length == 0)
                //    strPy = "#";
                //else
                //    strPy = info.strPinyin.Substring(0, 1).ToUpper();

                //tvFriends.Nodes.Add(node);
                int nSel = 0;
                if (nType == 2)         // 聊天群&已删除的服务号&其它
                    nSel = 1;
                else if (nType == 4 ||  // 该好友在群聊天中，但未添加好友
                         nType == 67 || // 语音提醒
                         nType == 6)    // 非好友
                    nSel = 2;
                if (info.strUsrName.Contains("gh_")) nSel = 2;  // 订阅号、服务号
                if (info.strUsrName.Contains("@chatroom")) nSel = 1;  // 群
                if (tvArr[nSel].Nodes.Find(strPy, false).Length == 0)
                    tvArr[nSel].Nodes.Add(strPy, strPy);
                tvArr[nSel].Nodes[strPy].Nodes.Add(node);
            }
            foreach (TreeView tv in tvArr)
            {
                tv.Sort();
                for (int i = 0; i < tv.Nodes.Count; i++)
                    tv.Nodes[i].ExpandAll();
                tv.Nodes[0].EnsureVisible();
            }

            connection.Close();
            progressForm.Hide();
            lbPageNumber.Text = "";

            // 释放 Emoji
            ReleaseEmoji();

            // 加载首页
            StreamToFile(m_strEmojiPath, ".", "default.html");
            wbHistory.WebView.Url = m_strEmojiPath + "default.html"; ;
        }

        /// <summary>
        /// 将资源文件中的 emoji 释放在临时文件夹中
        /// </summary>
        private void ReleaseEmoji()
        {
            // 释放路径
            string strEmojiPath = Path.GetTempFileName() + "_emoji\\";
            m_strEmojiPath = strEmojiPath.Replace(".tmp", "_tmp");
            m_strStyle = m_strStyle.Replace("%EMOJIPATH%", m_strEmojiPath);
            m_strStyle = m_strStyle.Replace("C:\\", "file:///C:/");
            m_strStyle = m_strStyle.Replace("\\", "/");
            Directory.CreateDirectory(m_strEmojiPath);
            for (int i = 0; i < 105; i++)
                StreamToFile(m_strEmojiPath, ".image.emoji.", i + ".png");
            StreamToFile(m_strEmojiPath, ".image.emoji.", "emoji17ced3.png");
        }

        /// <summary>
        /// 将 Stream 保存到指定位置
        /// </summary>
        /// <param name="strPath">保存文件的路径</param>
        /// <param name="strEmoji">文件名</param>
        private void StreamToFile(string strPath, string strResPath, string strEmoji)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string name = asm.GetName().Name;
            Stream stream = asm.GetManifestResourceStream(name + strResPath + strEmoji);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);

            string strFile = strPath + strEmoji;
            FileStream fs = new FileStream(strFile, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data);
            bw.Close();
            fs.Close();
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            tbSearchFriend.Left = 0;
            tbSearchFriend.Top = 0;
            btnSearchFriend.Left = splitContainer1.Panel1.Width - btnSearchFriend.Width;
            btnSearchFriend.Top = 0;
            tbSearchFriend.Width = btnSearchFriend.Left - 5;
            tabControl.Left = 0;
            tabControl.Top = btnSearchFriend.Top + btnSearchFriend.Height + 5;
            tabControl.Width = splitContainer1.Panel1.Width;
            tabControl.Height = splitContainer1.Panel1.Height - tbSearchFriend.Height - 5;
            tvFriends.Dock = DockStyle.Fill;
            tvGroup.Dock = DockStyle.Fill;
            tvOthers.Dock = DockStyle.Fill;
        }

        private void splitContainer1_Panel2_Resize(object sender, EventArgs e)
        {
            tbSearchHistory.Left = 0;
            tbSearchHistory.Top = 0;
            btnLast.Top = 0;
            btnFirst.Top = 0;
            btnBackward.Top = 0;
            btnForward.Top = 0;
            lbPageNumber.Top = 5;
            lbPageNumber.Left = splitContainer1.Panel2.Width - lbPageNumber.Width;
            btnLast.Left = lbPageNumber.Left - btnLast.Width - 5;
            btnForward.Left = btnLast.Left - btnForward.Width + 1;
            btnBackward.Left = btnForward.Left - btnBackward.Width + 1;
            btnFirst.Left = btnBackward.Left - btnFirst.Width + 1;
            btnSearchHistory.Left = btnFirst.Left - 5 - btnSearchHistory.Width;
            btnSearchHistory.Top = 0;
            cbSearchArea.Left = btnSearchHistory.Left - cbSearchArea.Width - 5;
            cbSearchArea.Top = 0;
            tbSearchHistory.Width = cbSearchArea.Left - 5;
            wbHistory.Left = 0;
            wbHistory.Top = tbSearchHistory.Top + tbSearchHistory.Height + 5;
            wbHistory.Width = splitContainer1.Panel2.Width;
            wbHistory.Height = splitContainer1.Panel2.Height - tbSearchHistory.Height - 5;
        }

        /// <summary>
        /// 计算 string 的 MD5 值
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        // Verify a hash against a string. 
        /// <summary>
        /// 比较 string 的 MD5 值是否与指定的 MD5 值一致
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input">被校验的string</param>
        /// <param name="hash">MD5参考值</param>
        /// <returns></returns>
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 复制文件夹（及文件夹下所有子文件夹和文件）
        /// </summary>
        /// <param name="sourcePath">待复制的文件夹路径</param>
        /// <param name="destinationPath">目标路径</param>
        public static void CopyDirectory(String sourcePath, String destinationPath)
        {
            DirectoryInfo info = new DirectoryInfo(sourcePath);
            Directory.CreateDirectory(destinationPath);
            foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
            {
                String destName = Path.Combine(destinationPath, fsi.Name);

                if (fsi is System.IO.FileInfo)          //如果是文件，复制文件
                    File.Copy(fsi.FullName, destName);
                else                                    //如果是文件夹，新建文件夹，递归
                {
                    Directory.CreateDirectory(destName);
                    CopyDirectory(fsi.FullName, destName);
                }
            }
        }

        /// <summary>
        /// 加载某好友的聊天记录
        /// </summary>
        /// <param name="info">要加载好友的信息</param>
        /// <param name="nPageCountOut">返回：聊天记录的总页数</param>
        /// <returns>返回聊天记录文件的路径</returns>
        private string LoadHistory(FriendInfo info, out int nPageCountOut)
        {
            string strUsrName = info.strUsrName;
            string strShowName = info.strRemarkName;
            if (info.strRemarkName.Length == 0)
                strShowName = info.strNickName;

            SQLiteConnection connection = new SQLiteConnection("Data Source=" + m_strSqlFile);
            connection.Open();
            MD5 md5Hash = MD5.Create();
            string strFriendMD5 = GetMd5Hash(md5Hash, strUsrName);
            string table = "Chat_" + strFriendMD5;
            SQLiteCommand cmd = new SQLiteCommand(connection);
            SQLiteDataReader dr;
            int nTotal = 0;
            try
            {
                // 获取记录总数
                cmd.CommandText = "select count(*) from " + table;
                dr = cmd.ExecuteReader();
                dr.Read();
                nTotal = dr.GetInt32(0);
                dr.Close();
                // 开始读取数据
                cmd.CommandText = "select datetime(createtime, 'unixepoch', 'localtime'),* from " + table;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SQLite.SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            string strHtml = "";
            strHtml = m_strStyle + m_strBody;
            string[] strDivArr = new string[m_nMaxRecord];
            int nCount = 0;
            int nPageCount = 0;
            string strLastFile = "";
            string strTmpPath = Path.GetTempFileName() + "\\";
            m_strTmpPath = strTmpPath.Replace(".tmp", "_tmp");
            Directory.CreateDirectory(m_strTmpPath);

            ProgressForm progressform = new ProgressForm();
            progressform.progressBar.Minimum = 0;
            progressform.progressBar.Maximum = nTotal;
            progressform.progressBar.Step = 1;
            progressform.Show();

            dr = cmd.ExecuteReader();
            int nDivIndex = 0;
            while (dr.Read())
            {
                progressform.progressBar.PerformStep();

                string strTime = dr.GetString(0);       // 产生时间
                string strMessage = dr.GetString(5);    // 消息内容
                int nType = dr.GetInt32(8);             // 消息类型
                int nDes = dr.GetInt32(9);              // 0-我发的  1-对方发的
                int nMesLocalID = dr.GetInt32(2);       // 消息的本地资源 ID

                switch (nType)
                {
                    case 1:     // 文字
                        break;
                    case 3:     // 图片
                        string strImgDir = m_strRoot + "Img\\" + strFriendMD5 + "\\";
                        string strImgSmall = strImgDir + nMesLocalID + ".pic_thum";
                        string strImgBig = strImgDir + nMesLocalID + ".pic";
                        if (File.Exists(strImgSmall) == false)
                            strImgSmall = strImgBig;
                        else if (File.Exists(strImgBig) == false)
                            strImgBig = strImgSmall;
                        if (File.Exists(strImgBig + "_hd"))  // 若有原图，默认显示原图
                            strMessage = "<IMG style=\"cursor:hand;max-width:150px;max-height:150px\" src=\""
                                + strImgSmall + "\" name=\"" + strImgBig + "_hd" + "\">";
                        else
                            strMessage = "<IMG style=\"cursor:hand;max-width:150px;max-height:150px\" src=\""
                                + strImgSmall + "\" name=\"" + strImgBig + "\">";
                        break;
                    case 43:    // 视频
                    case 62:    // 小视频
                        string strVideoDir = m_strRoot + "Video\\" + strFriendMD5 + "\\";
                        string strVideoThum = strVideoDir + nMesLocalID + ".video_thum";
                        string strVideo = strVideoDir + nMesLocalID + ".mp4";
                        string strPlayBtn = m_strPlayBtn.Replace("%REPLACE%", strVideo);
                        // 由于 block div 已经覆盖了图片的UI区域，所以不必像图片那样在 IMG 中添加视频路径
                        strMessage = strPlayBtn +
                            "<IMG style=\"cursor:hand;max-width:150px;max-height:150px\" src=\"" + strVideoThum + "\">";
                        break;
                    case 34:    // 语音
                        // 提取语音文件
                        string strAudioDir = m_strRoot + "Audio\\" + strFriendMD5 + "\\";
                        string strAudio = strAudioDir + nMesLocalID + ".aud";
                        // 提取语音长度
                        const string strKeyWord = "voicelength=\"";
                        int nStartIndex = strMessage.IndexOf(strKeyWord) + strKeyWord.Length;
                        string strAudioLen = strMessage.Substring(nStartIndex,
                            strMessage.IndexOf('\"', nStartIndex) - nStartIndex);
                        int nAudioLen = int.Parse(strAudioLen);
                        nAudioLen = nAudioLen / 1000 + (nAudioLen % 1000) / 500;  // 四舍五入
                        strAudioLen = nAudioLen + "\"";
                        // 根据语音长度增加泡泡的长度
                        int nWidth = 70 + (nAudioLen - 1) * 10;
                        nWidth = nWidth > 190 ? 190 : nWidth;
                        // 组织 HTML 语句
                        if (nDes == 0)
                        {
                            strAudioLen = m_strAudioLenR.Replace("%REPLACE%", strAudioLen);
                            strMessage = strAudioLen + m_strAudioIconR + m_strSpeechAudioR;
                            strMessage = strMessage.Replace("%REPLACE_WIDTH%", nWidth + "");
                            strMessage = strMessage.Replace("%REPLACE_NAME%", strAudio);
                        }
                        else
                        {
                            strAudioLen = m_strAudioLenL.Replace("%REPLACE%", strAudioLen);
                            strMessage = strAudioLen + m_strAudioIconL + m_strSpeechAudioL;
                            strMessage = strMessage.Replace("%REPLACE_WIDTH%", nWidth + "");
                            strMessage = strMessage.Replace("%REPLACE_NAME%", strAudio);
                        }
                        break;
                    case 49:    // 分享链接
                        string strType = "";
                        string strTitle = "";
                        string strDesc = "";
                        string strUrl = "";
                        try
                        {
                            // 解析 XML 数据
                            if (strMessage.Contains("<msg>"))
                                strMessage = strMessage.Substring(strMessage.IndexOf("<msg>"));
                            MemoryStream msXmlLink = new MemoryStream(Encoding.UTF8.GetBytes(strMessage));
                            XmlReader xmlLink = XmlReader.Create(msXmlLink);
                            while (xmlLink.Read())
                            {
                                if (xmlLink.NodeType == XmlNodeType.Element)
                                {
                                    if (xmlLink.Name == "type")
                                        strType = xmlLink.ReadString();
                                    else if (xmlLink.Name == "title")
                                        strTitle = xmlLink.ReadString();
                                    else if (xmlLink.Name == "des")
                                        strDesc = xmlLink.ReadString();
                                    else if (xmlLink.Name == "url")
                                        strUrl = xmlLink.ReadString();
                                }
                            }
                            xmlLink.Close();
                            msXmlLink.Close();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }

                        if (strType == "17")
                            strMessage = "<a style=\"color:#888\"> 实时位置信息... </a>";
                        else
                        {
                            //int nFirstIndex = strMessage.IndexOf("<title>") + 7;
                            //string strTitle = strMessage.Substring(nFirstIndex, strMessage.IndexOf("</title>") - nFirstIndex);
                            //nFirstIndex = strMessage.IndexOf("<des>") + 5;
                            //string strDesc = strMessage.Substring(nFirstIndex, strMessage.IndexOf("</des>") - nFirstIndex);
                            //nFirstIndex = strMessage.IndexOf("<url>") + 5;
                            //string strUrl = strMessage.Substring(nFirstIndex, strMessage.IndexOf("</url>") - nFirstIndex);
                            strMessage = string.Format("分享链接：<a href=\"{0}\" target=\"_blank\">{1}</br>" +
                                "<a style=\"color:#888\">{2}</a></a>", strUrl, strTitle, strDesc);
                        }
                        break;
                    case 48:     // 位置
                        string strX = "";
                        string strY = "";
                        string strLabel = "";
                        string strPoiname = "";
                        try
                        {
                            // 解析 XML 数据
                            if (strMessage.Contains("<msg>"))
                                strMessage = strMessage.Substring(strMessage.IndexOf("<msg>"));
                            MemoryStream msXmlLoc = new MemoryStream(Encoding.UTF8.GetBytes(strMessage));
                            XmlTextReader xmlLoc = new XmlTextReader(msXmlLoc);
                            while (xmlLoc.Read())
                            {
                                if (xmlLoc.NodeType == XmlNodeType.Element)
                                {
                                    if (xmlLoc.Name == "location")
                                    {
                                        strX = xmlLoc.GetAttribute("x");
                                        strY = xmlLoc.GetAttribute("y");
                                        strLabel = xmlLoc.GetAttribute("label");
                                        strPoiname = xmlLoc.GetAttribute("poiname");
                                        if (strLabel.Length == 0)
                                            strLabel = " ";
                                        if (strPoiname.Length == 0)
                                            strPoiname = " ";
                                    }
                                }
                            }
                            xmlLoc.Close();
                            msXmlLoc.Close();
                        }
                        catch (Exception ex) { }

                        strMessage = string.Format("<a target=\"_blank\" href=\"http://apis.map.qq.com/uri/v1/marker?" +
                            "marker=coord:{0},{1};title:{2};addr:{3}\">位置信息</a>", strX, strY, strPoiname, strLabel);
                        break;
                    case 50:    // 语音、视频通话
                        string strDuration = "";
                        try
                        {
                            // 解析 XML 数据
                            XmlReaderSettings settings = new XmlReaderSettings();
                            settings.ConformanceLevel = ConformanceLevel.Fragment;  // Tell the XmlReader to not be so picky
                            MemoryStream msXmlVoice = new MemoryStream(Encoding.UTF8.GetBytes(strMessage));
                            XmlReader xmlVoice = XmlReader.Create(msXmlVoice, settings);
                            while (xmlVoice.Read())
                            {
                                if (xmlVoice.NodeType == XmlNodeType.Element)
                                {
                                    if (xmlVoice.Name == "duration")
                                        strDuration = xmlVoice.ReadString();
                                }
                            }
                            xmlVoice.Close();
                            msXmlVoice.Close();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }

                        if (strDuration.Length == 0)
                            strDuration = "0";
                        int nDuration = int.Parse(strDuration);
                        int nHour = nDuration / 3600;
                        int nMin = nDuration / 60 - nHour * 60;
                        int nSec = nDuration - nHour * 3600 - nMin * 60;
                        if (nHour == 0)
                            strMessage = string.Format("<a style=\"color:#888\">通话时长：{0:00}:{1:00}</a>", nMin, nSec);
                        else
                            strMessage = string.Format("<a style=\"color:#888\">通话时长：{2:00}:{0:00}:{1:00}</a>", nMin, nSec, nHour);
                        break;
                    case 47:    // 表情（暂不可提取）
                        string strEmotPath = AppDomain.CurrentDomain.BaseDirectory + "emoticon1\\";
                        string strMD5 = "";
                        string strProductId = "";
                        try
                        {
                            // 解析 XML 数据
                            XmlReaderSettings settings = new XmlReaderSettings();
                            settings.ConformanceLevel = ConformanceLevel.Fragment;  // Tell the XmlReader to not be so picky
                            if (strMessage.Contains("<msg"))
                                strMessage = strMessage.Substring(strMessage.IndexOf("<msg"));
                            MemoryStream msXmlVoice = new MemoryStream(Encoding.UTF8.GetBytes(strMessage));
                            XmlReader xmlVoice = XmlReader.Create(msXmlVoice, settings);
                            while (xmlVoice.Read())
                            {
                                if (xmlVoice.NodeType == XmlNodeType.Element)
                                {
                                    if (xmlVoice.Name == "emoji")
                                    {
                                        strMD5 = xmlVoice.GetAttribute("md5");
                                        strProductId = xmlVoice.GetAttribute("productid");
                                        break;
                                    }
                                }
                            }
                            xmlVoice.Close();
                            msXmlVoice.Close();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                        bool bDisplay = true;
                        if (strMD5.Length == 0) bDisplay = false;
                        string strEmoji = strEmotPath + strMD5 + ".pic";
                        if (File.Exists(strEmoji) == false) bDisplay = false;
                        if (bDisplay)
                            strMessage = String.Format("<img width=\"150px\" src=\"{0}\">", strEmoji);
                        else
                            strMessage = "<a style=\"color:#888\">表情信息（暂不可提取）</a>";
                        break;
                    case 42:    // 名片
                        string strNick = "";
                        string strUserName = "";
                        try
                        {
                            // 解析 XML 数据
                            XmlReaderSettings settings = new XmlReaderSettings();
                            settings.ConformanceLevel = ConformanceLevel.Fragment;  // Tell the XmlReader to not be so picky
                            if (strMessage.Contains("<msg"))
                                strMessage = strMessage.Substring(strMessage.IndexOf("<msg"));
                            MemoryStream msXmlVoice = new MemoryStream(Encoding.UTF8.GetBytes(strMessage));
                            XmlReader xmlVoice = XmlReader.Create(msXmlVoice, settings);
                            while (xmlVoice.Read())
                            {
                                if (xmlVoice.NodeType == XmlNodeType.Element)
                                {
                                    if (xmlVoice.Name == "msg")
                                    {
                                        strNick = xmlVoice.GetAttribute("nickname");
                                        strUserName = xmlVoice.GetAttribute("username");
                                    }
                                }
                            }
                            xmlVoice.Close();
                            msXmlVoice.Close();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                        strMessage = string.Format("<a style=\"color:#888\">名片信息：</br>微信ID：{0}</br>微信名：{1}</a>",
                            strUserName, strNick);
                        break;
                }

                if (nDes == 0)
                {
                    string strUserTime = "";
                    if (strUsrName.Contains("@chatroom"))
                    {
                        strUserTime = m_strDivImgR.Replace("%REPLACE%", "我" + "  " + strTime);
                    }
                    else
                        strUserTime = m_strDivImgR.Replace("%REPLACE%", strTime);
                    string strSpeech = m_strSpeechR.Replace("%REPLACE%", strMessage);
                    // 将图片在居中泡泡
                    if (nType == 3 || nType == 43)
                        strSpeech = strSpeech.Replace("class=\"speech right\"", "class=\"speech right\" align=\"center\"");
                    // 语音的话格式不一样
                    if (nType == 34)
                        strSpeech = strMessage;
                    strDivArr[nDivIndex++] =
                        m_strDivR.Replace("%REPLACE%", strUserTime) + m_strDivR.Replace("%REPLACE%", strSpeech);
                }
                else
                {
                    string strUserTime = "";
                    if (strUsrName.Contains("@chatroom"))
                    {
                        // 群成员发消息显示成员，而非群名
                        string strOriMessage = dr.GetString(5);
                        int nColonIndex = strOriMessage.IndexOf(':');
                        string strUser = "";
                        if (nColonIndex >= 0)
                            strUser = strOriMessage.Substring(0, nColonIndex);
                        if (strUser.Length == 0)
                        {
                            // 解析 XML 数据
                            if (strOriMessage.Contains("<msg>"))
                            //strOriMessage = strOriMessage.Substring(strOriMessage.IndexOf("<msg>"));
                            {
                                MemoryStream msXmlLink = new MemoryStream(Encoding.UTF8.GetBytes(strOriMessage));
                                XmlReader xmlLink = XmlReader.Create(msXmlLink);
                                while (xmlLink.Read())
                                {
                                    if (xmlLink.NodeType == XmlNodeType.Element)
                                    {
                                        if (xmlLink.Name == "videomsg")
                                            strUser = xmlLink.GetAttribute("fromusername");
                                    }
                                }
                                xmlLink.Close();
                                msXmlLink.Close();
                            }
                        }
                        if (strUser.Length == 0)
                            strUser = "系统提示";
                        // 显示中文名，而非微信ID
                        bool bFound = false;
                        FriendInfo tmpInfo;// = m_FriendList[strUser];
                        if (m_FriendList.TryGetValue(strUser, out tmpInfo))  // 此处若用 try...catch 将导致性能大大降低！
                        {
                            strShowName = tmpInfo.strRemarkName;
                            if (tmpInfo.strRemarkName.Length == 0)
                                strShowName = tmpInfo.strNickName;
                            bFound = true;
                        }
                        if (bFound == false)
                            strShowName = strUser;
                        strUserTime = m_strDivImgL.Replace("%REPLACE%", strShowName + "  " + strTime);
                        //if (strMessage.Contains(strUser) == false)
                        //    strMessage = strUser + ": " + strMessage;
                    }
                    else
                        strUserTime = m_strDivImgL.Replace("%REPLACE%", strShowName + "  " + strTime);
                    string strSpeech = m_strSpeechL.Replace("%REPLACE%", strMessage);
                    // 将图片在居中泡泡
                    if (nType == 3 || nType == 43)
                        strSpeech = strSpeech.Replace("class=\"speech left\"", "class=\"speech left\" align=\"center\"");
                    // 语音的话格式不一样
                    if (nType == 34)
                        strSpeech = strMessage;
                    strDivArr[nDivIndex++] =
                        m_strDivL.Replace("%REPLACE%", strUserTime) + m_strDivL.Replace("%REPLACE%", strSpeech);
                }

                // 达到每页最大记录数，换页
                if (++nCount >= m_nMaxRecord)
                {
                    string strDivAll = "";
                    for (int i = 0; i < m_nMaxRecord; i++)
                    {
                        strDivAll += strDivArr[i];
                        strDivArr[i] = "";
                    }
                    strHtml = m_strStyle + m_strBody.Replace("%REPLACE%", strDivAll);
                    ReplaceEmoji(ref strHtml);  // 替换官方表情
                    nDivIndex = 0;
                    FileInfo fileInfo = new FileInfo(m_strTmpPath + "WechatHistory_" + ++nPageCount + ".html");
                    FileStream fs = fileInfo.Open(FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("UTF-8"));
                    sw.WriteLine(strHtml);
                    sw.Flush();
                    sw.Close();
                    strLastFile = fileInfo.FullName;

                    nCount = 0;
                }
            }
            if (nCount != 0)
            {
                string strDivAll = "";
                for (int i = 0; i < m_nMaxRecord; i++)
                {
                    strDivAll += strDivArr[i];
                    strDivArr[i] = "";
                }
                strHtml = m_strStyle + m_strBody.Replace("%REPLACE%", strDivAll);
                ReplaceEmoji(ref strHtml);  // 替换官方表情
                nDivIndex = 0;
                FileInfo fileInfo = new FileInfo(m_strTmpPath + "WechatHistory_" + ++nPageCount + ".html");
                FileStream fs = fileInfo.Open(FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("UTF-8"));
                sw.WriteLine(strHtml);
                sw.Flush();
                sw.Close();
                strLastFile = fileInfo.FullName;
            }
            progressform.Hide();
            nPageCountOut = nPageCount;
            return strLastFile;
        }


        private string GetEmojiFromByte(byte b1, byte b2)
        {
            byte[] bt = new byte[3];
            bt[0] = 0xEE;
            bt[1] = b1;
            bt[2] = b2;
            return System.Text.Encoding.UTF8.GetString(bt);
        }

        private string GetQQEmojiFromByte(byte b1, byte b2, byte b3)
        {
            byte[] bt = new byte[4];
            bt[0] = 0xF0;
            bt[1] = b1;
            bt[2] = b2;
            bt[3] = b3;
            return System.Text.Encoding.UTF8.GetString(bt);
        }

        /// <summary>
        /// 将聊天记录中的 emoji 标记替换为相应图片
        /// </summary>
        private void ReplaceEmoji(ref string str)
        {
            // 以下替换桌面/网页微信中自带的iOS官方EMOJI表情
            str = str.Replace(GetEmojiFromByte(0x90, 0x95), "<span class=\"emoji emoji1f604\"></span>");
            str = str.Replace(GetEmojiFromByte(0x81, 0x96), "<span class=\"emoji emoji1f60a\"></span>");
            str = str.Replace(GetEmojiFromByte(0x81, 0x97), "<span class=\"emoji emoji1f63a\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x94), "<span class=\"emoji emoji263a\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x85), "<span class=\"emoji emoji1f609\"></span>");
            str = str.Replace(GetEmojiFromByte(0x84, 0x86), "<span class=\"emoji emoji1f63b\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x98), "<span class=\"emoji emoji1f63d\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x97), "<span class=\"emoji emoji1f61a\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x8D), "<span class=\"emoji emoji1f633\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x84), "<span class=\"emoji emoji1f63c\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x8A), "<span class=\"emoji emoji1f60c\"></span>");
            str = str.Replace(GetEmojiFromByte(0x84, 0x85), "<span class=\"emoji emoji1f61c\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x89), "<span class=\"emoji emoji1f61d\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x8E), "<span class=\"emoji emoji1f612\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x82), "<span class=\"emoji emoji1f60f\"></span>");
            str = str.Replace(GetEmojiFromByte(0x84, 0x88), "<span class=\"emoji emoji1f613\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x83), "<span class=\"emoji emoji1f64d\"></span>");
            str = str.Replace(GetEmojiFromByte(0x81, 0x98), "<span class=\"emoji emoji1f61e\"></span>");
            str = str.Replace(GetEmojiFromByte(0x90, 0x87), "<span class=\"emoji emoji1f4ab\"></span>");
            // 以下替换手机微信表情中自带的iOS官方EMOJI表情
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x98, 0x84), "<span class=\"emoji emoji1f604\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x98, 0xB7), "<span class=\"emoji emoji1f637\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x98, 0x82), "<span class=\"emoji emoji1f639\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x98, 0x9D), "<span class=\"emoji emoji1f61d\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x98, 0xB2), "<span class=\"emoji emoji1f632\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x98, 0xB3), "<span class=\"emoji emoji1f633\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x98, 0xB1), "<span class=\"emoji emoji1f631\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x98, 0x94), "<span class=\"emoji emoji1f64d\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x98, 0x89), "<span class=\"emoji emoji1f609\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x98, 0x8C), "<span class=\"emoji emoji1f60c\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x98, 0x92), "<span class=\"emoji emoji1f612\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x91, 0xBF), "<span class=\"emoji emoji1f47f\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x91, 0xBB), "<span class=\"emoji emoji1f47b\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x92, 0x9D), "<span class=\"emoji emoji1f49d\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x99, 0x8F), "<span class=\"emoji emoji1f64f\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x92, 0xAA), "<span class=\"emoji emoji1f4aa\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x92, 0xB0), "<span class=\"emoji emoji1f4b5\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x8E, 0x82), "<span class=\"emoji emoji1f382\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x8E, 0x88), "<span class=\"emoji emoji1f388\"></span>");
            str = str.Replace(GetQQEmojiFromByte(0x9F, 0x8E, 0x81), "<span class=\"emoji emoji1f4e6\"></span>");

            // 无 QQ emoji 则退出
            int nStart = str.IndexOf('[');
            int nEnd = str.IndexOf(']');
            if (nEnd <= nStart) return;

            // QQ emoji
            str = str.Replace("[Smile]", "<img src=\"" + m_strEmojiPath + "\\0.png" + "\">");
            str = str.Replace("[微笑]", "<img src=\"" + m_strEmojiPath + "\\0.png" + "\">");
            str = str.Replace("[Grimace]", "<img src=\"" + m_strEmojiPath + "\\1.png" + "\">");
            str = str.Replace("[撇嘴]", "<img src=\"" + m_strEmojiPath + "\\1.png" + "\">");
            str = str.Replace("[Drool]", "<img src=\"" + m_strEmojiPath + "\\2.png" + "\">");
            str = str.Replace("[色]", "<img src=\"" + m_strEmojiPath + "\\2.png" + "\">");
            str = str.Replace("[Scowl]", "<img src=\"" + m_strEmojiPath + "\\3.png" + "\">");
            str = str.Replace("[发呆]", "<img src=\"" + m_strEmojiPath + "\\3.png" + "\">");
            str = str.Replace("[CoolGuy]", "<img src=\"" + m_strEmojiPath + "\\4.png" + "\">");
            str = str.Replace("[得意]", "<img src=\"" + m_strEmojiPath + "\\4.png" + "\">");
            str = str.Replace("[Sob]", "<img src=\"" + m_strEmojiPath + "\\5.png" + "\">");
            str = str.Replace("[流泪]", "<img src=\"" + m_strEmojiPath + "\\5.png" + "\">");
            str = str.Replace("[Shy]", "<img src=\"" + m_strEmojiPath + "\\6.png" + "\">");
            str = str.Replace("[害羞]", "<img src=\"" + m_strEmojiPath + "\\6.png" + "\">");
            str = str.Replace("[Silent]", "<img src=\"" + m_strEmojiPath + "\\7.png" + "\">");
            str = str.Replace("[闭嘴]", "<img src=\"" + m_strEmojiPath + "\\7.png" + "\">");
            str = str.Replace("[Sleep]", "<img src=\"" + m_strEmojiPath + "\\8.png" + "\">");
            str = str.Replace("[睡]", "<img src=\"" + m_strEmojiPath + "\\8.png" + "\">");
            str = str.Replace("[Cry]", "<img src=\"" + m_strEmojiPath + "\\9.png" + "\">");
            str = str.Replace("[大哭]", "<img src=\"" + m_strEmojiPath + "\\9.png" + "\">");
            str = str.Replace("[Awkward]", "<img src=\"" + m_strEmojiPath + "\\10.png" + "\">");
            str = str.Replace("[尴尬]", "<img src=\"" + m_strEmojiPath + "\\10.png" + "\">");
            str = str.Replace("[Angry]", "<img src=\"" + m_strEmojiPath + "\\11.png" + "\">");
            str = str.Replace("[发怒]", "<img src=\"" + m_strEmojiPath + "\\11.png" + "\">");
            str = str.Replace("[Tongue]", "<img src=\"" + m_strEmojiPath + "\\12.png" + "\">");
            str = str.Replace("[调皮]", "<img src=\"" + m_strEmojiPath + "\\12.png" + "\">");
            str = str.Replace("[Grin]", "<img src=\"" + m_strEmojiPath + "\\13.png" + "\">");
            str = str.Replace("[呲牙]", "<img src=\"" + m_strEmojiPath + "\\13.png" + "\">");
            str = str.Replace("[Surprise]", "<img src=\"" + m_strEmojiPath + "\\14.png" + "\">");
            str = str.Replace("[惊讶]", "<img src=\"" + m_strEmojiPath + "\\14.png" + "\">");
            str = str.Replace("[Frown]", "<img src=\"" + m_strEmojiPath + "\\15.png" + "\">");
            str = str.Replace("[难过]", "<img src=\"" + m_strEmojiPath + "\\15.png" + "\">");
            str = str.Replace("[Ruthless]", "<img src=\"" + m_strEmojiPath + "\\16.png" + "\">");
            str = str.Replace("[酷]", "<img src=\"" + m_strEmojiPath + "\\16.png" + "\">");
            str = str.Replace("[Blush]", "<img src=\"" + m_strEmojiPath + "\\17.png" + "\">");
            str = str.Replace("[冷汗]", "<img src=\"" + m_strEmojiPath + "\\17.png" + "\">");
            str = str.Replace("[Scream]", "<img src=\"" + m_strEmojiPath + "\\18.png" + "\">");
            str = str.Replace("[抓狂]", "<img src=\"" + m_strEmojiPath + "\\18.png" + "\">");
            str = str.Replace("[Puke]", "<img src=\"" + m_strEmojiPath + "\\19.png" + "\">");
            str = str.Replace("[吐]", "<img src=\"" + m_strEmojiPath + "\\19.png" + "\">");
            str = str.Replace("[Chuckle]", "<img src=\"" + m_strEmojiPath + "\\20.png" + "\">");
            str = str.Replace("[偷笑]", "<img src=\"" + m_strEmojiPath + "\\20.png" + "\">");
            str = str.Replace("[Joyful]", "<img src=\"" + m_strEmojiPath + "\\21.png" + "\">");
            str = str.Replace("[愉快]", "<img src=\"" + m_strEmojiPath + "\\21.png" + "\">");
            str = str.Replace("[Slight]", "<img src=\"" + m_strEmojiPath + "\\22.png" + "\">");
            str = str.Replace("[白眼]", "<img src=\"" + m_strEmojiPath + "\\22.png" + "\">");
            str = str.Replace("[Smug]", "<img src=\"" + m_strEmojiPath + "\\23.png" + "\">");
            str = str.Replace("[傲慢]", "<img src=\"" + m_strEmojiPath + "\\23.png" + "\">");
            str = str.Replace("[Hungry]", "<img src=\"" + m_strEmojiPath + "\\24.png" + "\">");
            str = str.Replace("[饥饿]", "<img src=\"" + m_strEmojiPath + "\\24.png" + "\">");
            str = str.Replace("[Drowsy]", "<img src=\"" + m_strEmojiPath + "\\25.png" + "\">");
            str = str.Replace("[困]", "<img src=\"" + m_strEmojiPath + "\\25.png" + "\">");
            str = str.Replace("[Panic]", "<img src=\"" + m_strEmojiPath + "\\26.png" + "\">");
            str = str.Replace("[惊恐]", "<img src=\"" + m_strEmojiPath + "\\26.png" + "\">");
            str = str.Replace("[Sweat]", "<img src=\"" + m_strEmojiPath + "\\27.png" + "\">");
            str = str.Replace("[流汗]", "<img src=\"" + m_strEmojiPath + "\\27.png" + "\">");
            str = str.Replace("[Laugh]", "<img src=\"" + m_strEmojiPath + "\\28.png" + "\">");
            str = str.Replace("[憨笑]", "<img src=\"" + m_strEmojiPath + "\\28.png" + "\">");
            str = str.Replace("[Commando]", "<img src=\"" + m_strEmojiPath + "\\29.png" + "\">");
            str = str.Replace("[悠闲]", "<img src=\"" + m_strEmojiPath + "\\29.png" + "\">");
            str = str.Replace("[Determined]", "<img src=\"" + m_strEmojiPath + "\\30.png" + "\">");
            str = str.Replace("[奋斗]", "<img src=\"" + m_strEmojiPath + "\\30.png" + "\">");
            str = str.Replace("[Scold]", "<img src=\"" + m_strEmojiPath + "\\31.png" + "\">");
            str = str.Replace("[咒骂]", "<img src=\"" + m_strEmojiPath + "\\31.png" + "\">");
            str = str.Replace("[Shocked]", "<img src=\"" + m_strEmojiPath + "\\32.png" + "\">");
            str = str.Replace("[疑问]", "<img src=\"" + m_strEmojiPath + "\\32.png" + "\">");
            str = str.Replace("[Shhh]", "<img src=\"" + m_strEmojiPath + "\\33.png" + "\">");
            str = str.Replace("[嘘]", "<img src=\"" + m_strEmojiPath + "\\33.png" + "\">");
            str = str.Replace("[Dizzy]", "<img src=\"" + m_strEmojiPath + "\\34.png" + "\">");
            str = str.Replace("[晕]", "<img src=\"" + m_strEmojiPath + "\\34.png" + "\">");
            str = str.Replace("[Tormented]", "<img src=\"" + m_strEmojiPath + "\\35.png" + "\">");
            str = str.Replace("[疯了]", "<img src=\"" + m_strEmojiPath + "\\35.png" + "\">");
            str = str.Replace("[Toasted]", "<img src=\"" + m_strEmojiPath + "\\36.png" + "\">");
            str = str.Replace("[衰]", "<img src=\"" + m_strEmojiPath + "\\36.png" + "\">");
            str = str.Replace("[Skull]", "<img src=\"" + m_strEmojiPath + "\\37.png" + "\">");
            str = str.Replace("[骷髅]", "<img src=\"" + m_strEmojiPath + "\\37.png" + "\">");
            str = str.Replace("[Hammer]", "<img src=\"" + m_strEmojiPath + "\\38.png" + "\">");
            str = str.Replace("[敲打]", "<img src=\"" + m_strEmojiPath + "\\38.png" + "\">");
            str = str.Replace("[Wave]", "<img src=\"" + m_strEmojiPath + "\\39.png" + "\">");
            str = str.Replace("[再见]", "<img src=\"" + m_strEmojiPath + "\\39.png" + "\">");
            str = str.Replace("[Speechless]", "<img src=\"" + m_strEmojiPath + "\\40.png" + "\">");
            str = str.Replace("[擦汗]", "<img src=\"" + m_strEmojiPath + "\\40.png" + "\">");
            str = str.Replace("[NosePick]", "<img src=\"" + m_strEmojiPath + "\\41.png" + "\">");
            str = str.Replace("[抠鼻]", "<img src=\"" + m_strEmojiPath + "\\41.png" + "\">");
            str = str.Replace("[Clap]", "<img src=\"" + m_strEmojiPath + "\\42.png" + "\">");
            str = str.Replace("[鼓掌]", "<img src=\"" + m_strEmojiPath + "\\42.png" + "\">");
            str = str.Replace("[Shame]", "<img src=\"" + m_strEmojiPath + "\\43.png" + "\">");
            str = str.Replace("[糗大了]", "<img src=\"" + m_strEmojiPath + "\\43.png" + "\">");
            str = str.Replace("[Trick]", "<img src=\"" + m_strEmojiPath + "\\44.png" + "\">");
            str = str.Replace("[坏笑]", "<img src=\"" + m_strEmojiPath + "\\44.png" + "\">");
            str = str.Replace("[Bah！L]", "<img src=\"" + m_strEmojiPath + "\\45.png" + "\">");
            str = str.Replace("[左哼哼]", "<img src=\"" + m_strEmojiPath + "\\45.png" + "\">");
            str = str.Replace("[Bah！R]", "<img src=\"" + m_strEmojiPath + "\\46.png" + "\">");
            str = str.Replace("[右哼哼]", "<img src=\"" + m_strEmojiPath + "\\46.png" + "\">");
            str = str.Replace("[Yawn]", "<img src=\"" + m_strEmojiPath + "\\47.png" + "\">");
            str = str.Replace("[哈欠]", "<img src=\"" + m_strEmojiPath + "\\47.png" + "\">");
            str = str.Replace("[Pooh-pooh]", "<img src=\"" + m_strEmojiPath + "\\48.png" + "\">");
            str = str.Replace("[鄙视]", "<img src=\"" + m_strEmojiPath + "\\48.png" + "\">");
            str = str.Replace("[Shrunken]", "<img src=\"" + m_strEmojiPath + "\\49.png" + "\">");
            str = str.Replace("[委屈]", "<img src=\"" + m_strEmojiPath + "\\49.png" + "\">");
            str = str.Replace("[TearingUp]", "<img src=\"" + m_strEmojiPath + "\\50.png" + "\">");
            str = str.Replace("[快哭了]", "<img src=\"" + m_strEmojiPath + "\\50.png" + "\">");
            str = str.Replace("[Sly]", "<img src=\"" + m_strEmojiPath + "\\51.png" + "\">");
            str = str.Replace("[阴险]", "<img src=\"" + m_strEmojiPath + "\\51.png" + "\">");
            str = str.Replace("[Kiss]", "<img src=\"" + m_strEmojiPath + "\\52.png" + "\">");
            str = str.Replace("[亲亲]", "<img src=\"" + m_strEmojiPath + "\\52.png" + "\">");
            str = str.Replace("[Wrath]", "<img src=\"" + m_strEmojiPath + "\\53.png" + "\">");
            str = str.Replace("[吓]", "<img src=\"" + m_strEmojiPath + "\\53.png" + "\">");
            str = str.Replace("[Whimper]", "<img src=\"" + m_strEmojiPath + "\\54.png" + "\">");
            str = str.Replace("[可怜]", "<img src=\"" + m_strEmojiPath + "\\54.png" + "\">");
            str = str.Replace("[Cleaver]", "<img src=\"" + m_strEmojiPath + "\\55.png" + "\">");
            str = str.Replace("[菜刀]", "<img src=\"" + m_strEmojiPath + "\\55.png" + "\">");
            str = str.Replace("[Watermelon]", "<img src=\"" + m_strEmojiPath + "\\56.png" + "\">");
            str = str.Replace("[西瓜]", "<img src=\"" + m_strEmojiPath + "\\56.png" + "\">");
            str = str.Replace("[Beer]", "<img src=\"" + m_strEmojiPath + "\\57.png" + "\">");
            str = str.Replace("[啤酒]", "<img src=\"" + m_strEmojiPath + "\\57.png" + "\">");
            str = str.Replace("[Basketball]", "<img src=\"" + m_strEmojiPath + "\\58.png" + "\">");
            str = str.Replace("[篮球]", "<img src=\"" + m_strEmojiPath + "\\58.png" + "\">");
            str = str.Replace("[PingPong]", "<img src=\"" + m_strEmojiPath + "\\59.png" + "\">");
            str = str.Replace("[乒乓]", "<img src=\"" + m_strEmojiPath + "\\59.png" + "\">");
            str = str.Replace("[Coffee]", "<img src=\"" + m_strEmojiPath + "\\60.png" + "\">");
            str = str.Replace("[咖啡]", "<img src=\"" + m_strEmojiPath + "\\60.png" + "\">");
            str = str.Replace("[Rice]", "<img src=\"" + m_strEmojiPath + "\\61.png" + "\">");
            str = str.Replace("[饭]", "<img src=\"" + m_strEmojiPath + "\\61.png" + "\">");
            str = str.Replace("[Pig]", "<img src=\"" + m_strEmojiPath + "\\62.png" + "\">");
            str = str.Replace("[猪头]", "<img src=\"" + m_strEmojiPath + "\\62.png" + "\">");
            str = str.Replace("[Rose]", "<img src=\"" + m_strEmojiPath + "\\63.png" + "\">");
            str = str.Replace("[玫瑰]", "<img src=\"" + m_strEmojiPath + "\\63.png" + "\">");
            str = str.Replace("[Wilt]", "<img src=\"" + m_strEmojiPath + "\\64.png" + "\">");
            str = str.Replace("[凋谢]", "<img src=\"" + m_strEmojiPath + "\\64.png" + "\">");
            str = str.Replace("[Lips]", "<img src=\"" + m_strEmojiPath + "\\65.png" + "\">");
            str = str.Replace("[嘴唇]", "<img src=\"" + m_strEmojiPath + "\\65.png" + "\">");
            str = str.Replace("[Heart]", "<img src=\"" + m_strEmojiPath + "\\66.png" + "\">");
            str = str.Replace("[爱心]", "<img src=\"" + m_strEmojiPath + "\\66.png" + "\">");
            str = str.Replace("[BrokenHeart]", "<img src=\"" + m_strEmojiPath + "\\67.png" + "\">");
            str = str.Replace("[心碎]", "<img src=\"" + m_strEmojiPath + "\\67.png" + "\">");
            str = str.Replace("[Cake]", "<img src=\"" + m_strEmojiPath + "\\68.png" + "\">");
            str = str.Replace("[蛋糕]", "<img src=\"" + m_strEmojiPath + "\\68.png" + "\">");
            str = str.Replace("[Lightning]", "<img src=\"" + m_strEmojiPath + "\\69.png" + "\">");
            str = str.Replace("[闪电]", "<img src=\"" + m_strEmojiPath + "\\69.png" + "\">");
            str = str.Replace("[Bomb]", "<img src=\"" + m_strEmojiPath + "\\70.png" + "\">");
            str = str.Replace("[炸弹]", "<img src=\"" + m_strEmojiPath + "\\70.png" + "\">");
            str = str.Replace("[Dagger]", "<img src=\"" + m_strEmojiPath + "\\71.png" + "\">");
            str = str.Replace("[刀]", "<img src=\"" + m_strEmojiPath + "\\71.png" + "\">");
            str = str.Replace("[Soccer]", "<img src=\"" + m_strEmojiPath + "\\72.png" + "\">");
            str = str.Replace("[足球]", "<img src=\"" + m_strEmojiPath + "\\72.png" + "\">");
            str = str.Replace("[Ladybug]", "<img src=\"" + m_strEmojiPath + "\\73.png" + "\">");
            str = str.Replace("[瓢虫]", "<img src=\"" + m_strEmojiPath + "\\73.png" + "\">");
            str = str.Replace("[Poop]", "<img src=\"" + m_strEmojiPath + "\\74.png" + "\">");
            str = str.Replace("[便便]", "<img src=\"" + m_strEmojiPath + "\\74.png" + "\">");
            str = str.Replace("[Moon]", "<img src=\"" + m_strEmojiPath + "\\75.png" + "\">");
            str = str.Replace("[月亮]", "<img src=\"" + m_strEmojiPath + "\\75.png" + "\">");
            str = str.Replace("[Sun]", "<img src=\"" + m_strEmojiPath + "\\76.png" + "\">");
            str = str.Replace("[太阳]", "<img src=\"" + m_strEmojiPath + "\\76.png" + "\">");
            str = str.Replace("[Gift]", "<img src=\"" + m_strEmojiPath + "\\77.png" + "\">");
            str = str.Replace("[礼物]", "<img src=\"" + m_strEmojiPath + "\\77.png" + "\">");
            str = str.Replace("[Hug]", "<img src=\"" + m_strEmojiPath + "\\78.png" + "\">");
            str = str.Replace("[拥抱]", "<img src=\"" + m_strEmojiPath + "\\78.png" + "\">");
            str = str.Replace("[ThumbsUp]", "<img src=\"" + m_strEmojiPath + "\\79.png" + "\">");
            str = str.Replace("[强]", "<img src=\"" + m_strEmojiPath + "\\79.png" + "\">");
            str = str.Replace("[ThumbsDown]", "<img src=\"" + m_strEmojiPath + "\\80.png" + "\">");
            str = str.Replace("[弱]", "<img src=\"" + m_strEmojiPath + "\\80.png" + "\">");
            str = str.Replace("[Shake]", "<img src=\"" + m_strEmojiPath + "\\81.png" + "\">");
            str = str.Replace("[握手]", "<img src=\"" + m_strEmojiPath + "\\81.png" + "\">");
            str = str.Replace("[Peace]", "<img src=\"" + m_strEmojiPath + "\\82.png" + "\">");
            str = str.Replace("[胜利]", "<img src=\"" + m_strEmojiPath + "\\82.png" + "\">");
            str = str.Replace("[Fight]", "<img src=\"" + m_strEmojiPath + "\\83.png" + "\">");
            str = str.Replace("[抱拳]", "<img src=\"" + m_strEmojiPath + "\\83.png" + "\">");
            str = str.Replace("[Beckon]", "<img src=\"" + m_strEmojiPath + "\\84.png" + "\">");
            str = str.Replace("[勾引]", "<img src=\"" + m_strEmojiPath + "\\84.png" + "\">");
            str = str.Replace("[Fist]", "<img src=\"" + m_strEmojiPath + "\\85.png" + "\">");
            str = str.Replace("[拳头]", "<img src=\"" + m_strEmojiPath + "\\85.png" + "\">");
            str = str.Replace("[Pinky]", "<img src=\"" + m_strEmojiPath + "\\86.png" + "\">");
            str = str.Replace("[差劲]", "<img src=\"" + m_strEmojiPath + "\\86.png" + "\">");
            str = str.Replace("[RockOn]", "<img src=\"" + m_strEmojiPath + "\\87.png" + "\">");
            str = str.Replace("[爱你]", "<img src=\"" + m_strEmojiPath + "\\87.png" + "\">");
            str = str.Replace("[Nuh-uh]", "<img src=\"" + m_strEmojiPath + "\\88.png" + "\">");
            str = str.Replace("[NO]", "<img src=\"" + m_strEmojiPath + "\\88.png" + "\">");
            //str = str.Replace("[OK]", "<img src=\"" + m_strEmojiPath + "\\89.png" + "\">");
            str = str.Replace("[OK]", "<img src=\"" + m_strEmojiPath + "\\89.png" + "\">");
            str = str.Replace("[InLove]", "<img src=\"" + m_strEmojiPath + "\\90.png" + "\">");
            str = str.Replace("[爱情]", "<img src=\"" + m_strEmojiPath + "\\90.png" + "\">");
            str = str.Replace("[Blowkiss]", "<img src=\"" + m_strEmojiPath + "\\91.png" + "\">");
            str = str.Replace("[飞吻]", "<img src=\"" + m_strEmojiPath + "\\91.png" + "\">");
            str = str.Replace("[Waddle]", "<img src=\"" + m_strEmojiPath + "\\92.png" + "\">");
            str = str.Replace("[跳跳]", "<img src=\"" + m_strEmojiPath + "\\92.png" + "\">");
            str = str.Replace("[Tremble]", "<img src=\"" + m_strEmojiPath + "\\93.png" + "\">");
            str = str.Replace("[发抖]", "<img src=\"" + m_strEmojiPath + "\\93.png" + "\">");
            str = str.Replace("[Aaagh!]", "<img src=\"" + m_strEmojiPath + "\\94.png" + "\">");
            str = str.Replace("[怄火]", "<img src=\"" + m_strEmojiPath + "\\94.png" + "\">");
            str = str.Replace("[Twirl]", "<img src=\"" + m_strEmojiPath + "\\95.png" + "\">");
            str = str.Replace("[转圈]", "<img src=\"" + m_strEmojiPath + "\\95.png" + "\">");
            str = str.Replace("[Kotow]", "<img src=\"" + m_strEmojiPath + "\\96.png" + "\">");
            str = str.Replace("[磕头]", "<img src=\"" + m_strEmojiPath + "\\96.png" + "\">");
            str = str.Replace("[Dramatic]", "<img src=\"" + m_strEmojiPath + "\\97.png" + "\">");
            str = str.Replace("[回头]", "<img src=\"" + m_strEmojiPath + "\\97.png" + "\">");
            str = str.Replace("[JumpRope]", "<img src=\"" + m_strEmojiPath + "\\98.png" + "\">");
            str = str.Replace("[跳绳]", "<img src=\"" + m_strEmojiPath + "\\98.png" + "\">");
            str = str.Replace("[Surrender]", "<img src=\"" + m_strEmojiPath + "\\99.png" + "\">");
            str = str.Replace("[投降]", "<img src=\"" + m_strEmojiPath + "\\99.png" + "\">");
            str = str.Replace("[Hooray]", "<img src=\"" + m_strEmojiPath + "\\100.png" + "\">");
            str = str.Replace("[激动]", "<img src=\"" + m_strEmojiPath + "\\100.png" + "\">");
            str = str.Replace("[Meditate]", "<img src=\"" + m_strEmojiPath + "\\101.png" + "\">");
            str = str.Replace("[街舞]", "<img src=\"" + m_strEmojiPath + "\\101.png" + "\">");
            str = str.Replace("[Smooch]", "<img src=\"" + m_strEmojiPath + "\\102.png" + "\">");
            str = str.Replace("[献吻]", "<img src=\"" + m_strEmojiPath + "\\102.png" + "\">");
            str = str.Replace("[TaiChi L]", "<img src=\"" + m_strEmojiPath + "\\103.png" + "\">");
            str = str.Replace("[左太极]", "<img src=\"" + m_strEmojiPath + "\\103.png" + "\">");
            str = str.Replace("[TaiChi R]", "<img src=\"" + m_strEmojiPath + "\\104.png" + "\">");
            str = str.Replace("[右太极]", "<img src=\"" + m_strEmojiPath + "\\104.png" + "\">");
        }

        /// <summary>
        /// 选择某一个好友后，显示其聊天记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFriends_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) return;

            FriendInfo info = (FriendInfo)e.Node.Tag;
            int nPageCount = 0;
            string strLastFile = LoadHistory(info, out nPageCount);
            m_strLastFriend = info.strUsrName;
            m_nMaxPage = nPageCount;
            m_nCurPage = nPageCount;
            wbHistory.WebView.Url = strLastFile;
            setNaviEnable();

            m_nLastClickTab = tabControl.SelectedIndex;
        }

        /// <summary>
        /// 给其它窗口提供的接口，显示指定好友聊天记录中的某一页
        /// </summary>
        /// <param name="info"></param>
        public void SetHistory(FriendInfo info, int nPage, string strKeyWord, string strContext)
        {
            int nPageCount = 0;
            if (m_strLastFriend != info.strUsrName)
            {
                LoadHistory(info, out nPageCount);
                m_nMaxPage = nPageCount;
            }
            m_strLastFriend = info.strUsrName;
            m_nCurPage = nPage;
            wbHistory.WebView.LoadUrlAndWait(m_strTmpPath + "WechatHistory_" + m_nCurPage + ".html");
            setNaviEnable();
            m_nLastClickTab = tabControl.SelectedIndex;
            // 以下两条语句使得一页出现多个关键字时，准确定位
            wbHistory.WebView.StartFindSession(strContext, false);
            wbHistory.WebView.StartFindSession(strKeyWord, false);
        }

        #region 聊天记录翻页按钮特效

        private void btnBackward_MouseDown(object sender, MouseEventArgs e)
        {
            btnBackward.Image = WechatHistory.Properties.Resources.backward_pressed;
        }
        private void btnBackward_MouseUp(object sender, MouseEventArgs e)
        {
            btnBackward.Image = WechatHistory.Properties.Resources.backward;
        }
        private void btnForward_MouseDown(object sender, MouseEventArgs e)
        {
            btnForward.Image = WechatHistory.Properties.Resources.forward_pressed;
        }
        private void btnForward_MouseUp(object sender, MouseEventArgs e)
        {
            btnForward.Image = WechatHistory.Properties.Resources.forward;
        }
        private void btnFirst_MouseDown(object sender, MouseEventArgs e)
        {
            btnFirst.Image = WechatHistory.Properties.Resources.first_pressed;
        }
        private void btnFirst_MouseUp(object sender, MouseEventArgs e)
        {
            btnFirst.Image = WechatHistory.Properties.Resources.first;
        }
        private void btnLast_MouseDown(object sender, MouseEventArgs e)
        {
            btnLast.Image = WechatHistory.Properties.Resources.last_pressed;
        }
        private void btnLast_MouseUp(object sender, MouseEventArgs e)
        {
            btnLast.Image = WechatHistory.Properties.Resources.last;
        }

        #endregion

        /// <summary>
        /// 把之前的好友聊天记录删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFriends_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (m_nMaxPage != 0)
            {
                try
                {
                    Directory.Delete(m_strTmpPath, true);
                }
                catch (Exception ex) { }
            }
        }

        /// <summary>
        /// 切换到上一页聊天记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackward_Click(object sender, EventArgs e)
        {
            m_nCurPage = m_nCurPage - 1 < 1 ? 1 : m_nCurPage - 1;
            string strWeb = m_strTmpPath + "WechatHistory_" + m_nCurPage + ".html";
            if (File.Exists(strWeb))
                wbHistory.WebView.Url = strWeb;
            wbHistory.Focus();
            setNaviEnable();
        }

        /// <summary>
        /// 切换到下一页聊天记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnForward_Click(object sender, EventArgs e)
        {
            m_nCurPage = m_nCurPage + 1 > m_nMaxPage ? m_nMaxPage : m_nCurPage + 1;
            string strWeb = m_strTmpPath + "WechatHistory_" + m_nCurPage + ".html";
            if (File.Exists(strWeb))
                wbHistory.WebView.Url = strWeb;
            wbHistory.Focus();
            setNaviEnable();
        }

        /// <summary>
        /// 切换到第一页聊天记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            m_nCurPage = 1;
            if (File.Exists(m_strTmpPath + "WechatHistory_1.html"))
                wbHistory.WebView.Url = m_strTmpPath + "WechatHistory_1.html";
            wbHistory.Focus();
            setNaviEnable();
        }

        /// <summary>
        /// 切换到最后一页聊天记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            m_nCurPage = m_nMaxPage;
            string strWeb = m_strTmpPath + "WechatHistory_" + m_nMaxPage + ".html";
            if (File.Exists(strWeb))
                wbHistory.WebView.Url = strWeb;
            wbHistory.Focus();
            setNaviEnable();
        }

        /// <summary>
        /// 根据当前聊天记录页数，设置导航按钮是否启用，同时刷新页码
        /// </summary>
        protected void setNaviEnable()
        {
            if (m_nCurPage == 1)
            {
                btnFirst.Enabled = false;
                btnBackward.Enabled = false;
            }
            else
            {
                btnFirst.Enabled = true;
                btnBackward.Enabled = true;
            }
            if (m_nCurPage == m_nMaxPage)
            {
                btnLast.Enabled = false;
                btnForward.Enabled = false;
            }
            else
            {
                btnLast.Enabled = true;
                btnForward.Enabled = true;
            }
            // 刷新页码
            lbPageNumber.Text = m_nCurPage + "/" + m_nMaxPage;
            splitContainer1_Panel2_Resize(null, null);
        }

        private void wbView_NewWindowEvent(object sender, NewWindowEventArgs e)
        {
        }

        /// <summary>
        /// 处理鼠标单击网页的图片、视频等元素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wbView_MouseClick(object sender, MouseEventArgs e)
        {
            // 只处理左键单击
            if (((MouseEventArgs)e).Button != System.Windows.Forms.MouseButtons.Left)
                return;
            Point pt = new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y);

            // 点击分享链接
            String target = ((EO.WebBrowser.WebView)sender).StatusMessage;
            if (target != "" && target != null && target.ToString().CompareTo("about:blank") != 0)
            {
                System.Diagnostics.Process.Start(target.ToString());
                return;
            }

            string strPicPath = "";
            try
            {
                strPicPath = (string)wbHistory.WebView.EvalScript(
                    String.Format("document.elementFromPoint({0}, {1}).getAttribute(\"Name\")",
                    ((MouseEventArgs)e).X, ((MouseEventArgs)e).Y));
            }
            catch (Exception ex) { }
            if (strPicPath == null) return;
            // 打开图片
            if (strPicPath.Contains(".pic"))
                System.Diagnostics.Process.Start("rundll32.exe",
                    string.Format("{0} {1}", "shimgvw.dll,ImageView_Fullscreen", strPicPath));
            // 打开视频
            else if (strPicPath.Contains(".mp4"))
            {
                if (File.Exists(strPicPath))
                    System.Diagnostics.Process.Start(strPicPath);
            }
            // 打开语音
            else if (strPicPath.Contains(".aud"))
            {
                try
                {
                    bool bIsAmr = true;
                    // 识别音频文件：amr or silk
                    FileStream fsAudio = File.OpenRead(strPicPath);
                    byte[] btHeader = new byte[32];
                    fsAudio.Read(btHeader, 0, btHeader.Length);
                    if (Encoding.ASCII.GetString(btHeader).Contains("SILK_V3"))  // SILK 格式
                        bIsAmr = false;
                    fsAudio.Close();
                    if (bIsAmr)  // AMR 文件
                    {
                        // 将 aud 文件转换成 amr 文件
                        string strAmrFile = Path.GetTempFileName();
                        FileStream fsw = File.OpenWrite(strAmrFile);
                        fsw.Write(Encoding.ASCII.GetBytes("#!AMR\n"), 0, 6);  // amr 文件头
                        FileStream fsr = File.OpenRead(strPicPath);
                        byte[] b = new byte[1024];
                        int nReadLen = 0;
                        while ((nReadLen = fsr.Read(b, 0, b.Length)) > 0)
                            fsw.Write(b, 0, nReadLen);
                        fsw.Flush();
                        fsw.Close();
                        fsr.Close();
                        // 使用 ffmpeg 转换成 wav 文件
                        string strWavFile = strAmrFile + ".wav";
                        string strFfmpeg = AppDomain.CurrentDomain.BaseDirectory + "ffmpeg\\ffmpeg.exe";
                        System.Diagnostics.Process proFfmpeg = new System.Diagnostics.Process();
                        proFfmpeg.StartInfo.FileName = strFfmpeg;
                        proFfmpeg.StartInfo.Arguments = string.Format("-y -i {0} {1}", strAmrFile, strWavFile);
                        proFfmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        proFfmpeg.Start();
                        proFfmpeg.WaitForExit();
                        // 播放 wav 文件
                        System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(strWavFile);
                        sndPlayer.Play();
                    }
                    else  // SILK 文件
                    {
                        // 将 aud 文件转换成 silk 文件：去掉第一个字节
                        string strSilkFile = Path.GetTempFileName();
                        FileStream fsw = File.OpenWrite(strSilkFile);
                        FileStream fsr = File.OpenRead(strPicPath);
                        fsr.ReadByte();  // 去掉第一个字节
                        byte[] b = new byte[1024];
                        int nReadLen = 0;
                        while ((nReadLen = fsr.Read(b, 0, b.Length)) > 0)
                            fsw.Write(b, 0, nReadLen);
                        fsw.Flush();
                        fsw.Close();
                        fsr.Close();
                        // 使用 SILK decoder 转换成 pcm 文件，SILK decoder：https://github.com/Voxer/silk-arm-ios
                        string strPcmFile = strSilkFile + ".pcm";
                        string strSilkCodec = AppDomain.CurrentDomain.BaseDirectory + "ffmpeg\\SilkDecoder.exe";
                        System.Diagnostics.Process proSilkCodec = new System.Diagnostics.Process();
                        proSilkCodec.StartInfo.FileName = strSilkCodec;
                        proSilkCodec.StartInfo.Arguments = string.Format("{0} {1}", strSilkFile, strPcmFile);
                        proSilkCodec.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        proSilkCodec.Start();
                        proSilkCodec.WaitForExit();
                        // 使用 ffmpeg 将 pcm 转换成 wav 文件
                        string strWavFile = strSilkFile + ".wav";
                        string strFfmpeg = AppDomain.CurrentDomain.BaseDirectory + "ffmpeg\\ffmpeg.exe";
                        System.Diagnostics.Process proFfmpeg = new System.Diagnostics.Process();
                        proFfmpeg.StartInfo.FileName = strFfmpeg;
                        proFfmpeg.StartInfo.Arguments = string.Format(
                            "-y -f s16le -ar 11780 -ac 2 -i {0} -ar 11780 -ac 2 {1}", strPcmFile, strWavFile);
                        proFfmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        proFfmpeg.Start();
                        proFfmpeg.WaitForExit();
                        // 播放 wav 文件
                        System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(strWavFile);
                        sndPlayer.Play();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tvFriends_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // 记录当前页选择的结点
            int nSel = tabControl.SelectedIndex;
            TreeView[] tvArr = new TreeView[3];
            tvArr[0] = tvFriends;
            tvArr[1] = tvGroup;
            tvArr[2] = tvOthers;
            if (tvArr[nSel].SelectedNode != null)
                m_strSelectedNodeArr[nSel] = tvArr[nSel].SelectedNode.Text;
            // 当切换 tab 时，只有选择的结点未改变才触发 AfterSelect，避免 AfterSelect 执行 2 次
            if (nSel != m_nLastClickTab &&
                e.Node.Text == m_strSelectedNodeArr[nSel])
            {
                TreeViewEventArgs tve = new TreeViewEventArgs(e.Node, TreeViewAction.ByMouse);
                tvFriends_AfterSelect(sender, tve);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int nSel = tabControl.SelectedIndex;
            //TreeView[] tvArr = new TreeView[3];
            //tvArr[0] = tvFriends;
            //tvArr[1] = tvGroup;
            //tvArr[2] = tvOthers;
            //tvArr[nSel].SelectedNode = null;
        }

        private void lbPageNumber_Click(object sender, EventArgs e)
        {
            Jump2PageForm jumpForm = new Jump2PageForm();
            jumpForm.label.Text = string.Format("跳至页码(1 - {0}):", m_nMaxPage);
            jumpForm.m_nMaxPage = m_nMaxPage;
            if (jumpForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_nCurPage = jumpForm.m_nPage;
                wbHistory.WebView.Url = m_strTmpPath + "WechatHistory_" + m_nCurPage + ".html";
                setNaviEnable();
            }
        }

        private void tbSearchFriend_Leave(object sender, EventArgs e)
        {
            if (tbSearchFriend.Text.Trim().Length == 0)
            {
                tbSearchFriend.Text = "搜索好友";
                tbSearchFriend.ForeColor = Color.LightGray;
            }
        }

        private void tbSearchFriend_Enter(object sender, EventArgs e)
        {
            if (tbSearchFriend.Text == "搜索好友")
            {
                tbSearchFriend.Text = "";
                tbSearchFriend.ForeColor = Color.Black;
            }
        }

        private void tbSearchFriend_TextChanged(object sender, EventArgs e)
        {
            string strSearch = tbSearchFriend.Text;
            if (strSearch.Length == 0 || tbSearchFriend.ForeColor == Color.LightGray)
            {
                m_fmFriendSearchResult.Hide();
                return;
            }

            // 遍历搜索结点
            List<TreeNode> foundNodes = new List<TreeNode>();
            TreeView[] tvArr = new TreeView[3];
            tvArr[0] = tvFriends;
            tvArr[1] = tvGroup;
            tvArr[2] = tvOthers;
            foreach (TreeView tv in tvArr)
            {
                foreach (TreeNode node in tv.Nodes)
                {
                    foreach (TreeNode subNode in node.Nodes)
                    {
                        FriendInfo info = (FriendInfo)subNode.Tag;
                        if (subNode.Text.Contains(strSearch))
                            foundNodes.Add(subNode);
                        else if (info.strPinyin.ToLower().Contains(strSearch.ToLower()))
                            foundNodes.Add(subNode);
                    }
                }
            }
            // 显示搜索结果
            m_fmFriendSearchResult.listView.Items.Clear();
            if (foundNodes.Count() != 0)
            {
                foreach (TreeNode node in foundNodes)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = node.Text;
                    item.Tag = node;
                    m_fmFriendSearchResult.listView.Items.Add(item);
                }
                m_fmFriendSearchResult.Show();
            }
            m_fmFriendSearchResult.Left = splitContainer1.Panel2.Left + this.Left;
            m_fmFriendSearchResult.Top = wbHistory.Top + this.Top;
            m_fmFriendSearchResult.TopMost = true;
            // 保持持续输入
            this.Focus();
            m_fmFriendSearchResult.Opacity = 1;
        }

        public static void SelectNode(TreeNode node)
        {
            ((TabControl)node.TreeView.Parent.Parent).SelectedTab = ((TabPage)node.TreeView.Parent);
            node.TreeView.SelectedNode = null;
            node.TreeView.SelectedNode = node;
        }

        private void btnSearchHistory_Click(object sender, EventArgs e)
        {
            string strSearch = tbSearchHistory.Text;
            if (strSearch.Trim().Length == 0 || strSearch.Trim() == "搜索聊天记录") return;

            if (cbSearchArea.SelectedIndex == 0)  // 搜索当前好友
            {
                int nSel = tabControl.SelectedIndex;
                TreeView[] tvArr = new TreeView[3];
                tvArr[0] = tvFriends;
                tvArr[1] = tvGroup;
                tvArr[2] = tvOthers;
                if (tvArr[nSel].SelectedNode == null)
                {
                    MessageBox.Show("未选中任何好友，将无法搜索。");
                    return;
                }
                FriendInfo info = (FriendInfo)tvArr[nSel].SelectedNode.Tag;
                Dictionary<string, FriendInfo> friendList = new Dictionary<string, FriendInfo>();
                friendList.Add(info.strUsrName, info);

                // 获取当前好友的消息记录数
                SQLiteConnection connection = new SQLiteConnection("Data Source=" + m_strSqlFile);
                connection.Open();
                MD5 md5Hash = MD5.Create();
                string strFriendMD5 = GetMd5Hash(md5Hash, info.strUsrName);
                string table = "Chat_" + strFriendMD5;
                SQLiteCommand cmd = new SQLiteCommand(connection);
                SQLiteDataReader dr;
                try
                {
                    // 开始读取数据
                    cmd.CommandText = "select count(*) from " + table;
                    cmd.ExecuteNonQuery();
                }
                catch (System.Data.SQLite.SQLiteException ex)
                {
                }
                dr = cmd.ExecuteReader();
                dr.Read();
                int nCount = dr.GetInt32(0);

                // 开始搜索
                ProgressForm fmProgress = new ProgressForm();
                fmProgress.Show();
                fmProgress.progressBar.Minimum = 0;
                fmProgress.progressBar.Maximum = nCount;
                fmProgress.progressBar.Step = 1;
                int num = SearchHistory(strSearch, friendList, fmProgress);
                fmProgress.Close();
                MessageBox.Show(string.Format("已搜索到{0}条消息记录。", num));
            }  // end of if: 搜索当前好友
            else  // 搜索所有好友
            {
                ProgressForm fmProgress = new ProgressForm();
                fmProgress.Show();
                fmProgress.progressBar.Minimum = 0;
                fmProgress.progressBar.Maximum = m_FriendList.Count();
                fmProgress.progressBar.Step = 1;
                int num = SearchHistory(strSearch, m_FriendList, fmProgress);
                fmProgress.Close();
                MessageBox.Show(string.Format("已搜索到{0}条消息记录。", num));
            }  // end of else: 搜索所有好友
        }

        /// <summary>
        /// 根据关键字搜索当前或所有好友的聊天记录，并显示
        /// </summary>
        /// <param name="strSearch">关键字</param>
        /// <param name="friendList">搜索对象链表</param>
        /// <param name="fmProgress">搜索过程中步进的进度窗口</param>
        /// <returns>搜索到的记录数</returns>
        private int SearchHistory(string strSearch, Dictionary<string, FriendInfo> friendList, ProgressForm fmProgress)
        {
            bool bAll = true;  // 当前是否搜索所有好友
            if (friendList.Count == 1) bAll = false;
            // 存储找到的聊天记录内容，并保存起来，其中：
            //   FriendInfo - 与该好友的聊天记录中存在搜索关键字
            //   string - 关键字
            //   int - 关键字所在的页码
            //   string - 将关键字的前后聊天内容显示在列表中
            List<KeyValuePair<KeyValuePair<FriendInfo, string>, KeyValuePair<int, string>>> foundList =
                new List<KeyValuePair<KeyValuePair<FriendInfo, string>, KeyValuePair<int, string>>>();
            foreach (KeyValuePair<string, FriendInfo> kvp in friendList)
            {
                string strUsrName = kvp.Value.strUsrName;

                if (bAll)
                    fmProgress.progressBar.PerformStep();
                SQLiteConnection connection = new SQLiteConnection("Data Source=" + m_strSqlFile);
                connection.Open();
                MD5 md5Hash = MD5.Create();
                string strFriendMD5 = GetMd5Hash(md5Hash, strUsrName);
                string table = "Chat_" + strFriendMD5;
                SQLiteCommand cmd = new SQLiteCommand(connection);
                SQLiteDataReader dr;
                //int nTotal = 0;
                try
                {
                    // 开始读取数据
                    cmd.CommandText = "select message from " + table;
                    cmd.ExecuteNonQuery();
                }
                catch (System.Data.SQLite.SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                dr = cmd.ExecuteReader();
                int nCount = 0;
                int nPage = 1;
                while (dr.Read())
                {
                    string strMessage = dr.GetString(0);    // 消息内容
                    if (bAll == false)
                        fmProgress.progressBar.PerformStep();

                    if (strMessage.ToLower().Contains(strSearch.ToLower()))
                    {
                        const int nShowLen = 20;
                        int nHead = strMessage.IndexOf(strSearch) - nShowLen;
                        int nTail = strMessage.IndexOf(strSearch) + strSearch.Length + nShowLen;
                        string strHead = "...";
                        string strTail = "...";
                        if (nHead < 0)
                        {
                            strHead = "";
                            nHead = 0;
                        }
                        if (nTail > strMessage.Length)
                        {
                            strTail = "";
                            nTail = strMessage.Length;
                        }
                        strMessage = strHead + strMessage.Substring(nHead, nTail - nHead) + strTail;
                        KeyValuePair<int, string> kvpValue = new KeyValuePair<int, string>(nPage, strMessage);
                        KeyValuePair<FriendInfo, string> kvpKey = new KeyValuePair<FriendInfo, string>(kvp.Value, strSearch);
                        KeyValuePair<KeyValuePair<FriendInfo, string>, KeyValuePair<int, string>> kvpNode =
                            new KeyValuePair<KeyValuePair<FriendInfo, string>, KeyValuePair<int, string>>(kvpKey, kvpValue);
                        //KeyValuePair<string, string> kvpNode = new KeyValuePair<string, string>(strUsrName, strMessage);
                        foundList.Add(kvpNode);
                    }  // end of if (strMessage.Contains(strSearch))

                    // 达到每页最大记录数，换页
                    if (++nCount >= m_nMaxRecord)
                    {
                        nPage++;
                        nCount = 0;
                    }
                }  // end of while (dr.Read())
            }  // end of foreach
            MessageSearchResultsForm msgResFm = new MessageSearchResultsForm(this);
            foreach (KeyValuePair<KeyValuePair<FriendInfo, string>, KeyValuePair<int, string>> kvpNode in foundList)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = "与“" + kvpNode.Key.Key.strDisplayName + "”的聊天记录中：" + kvpNode.Value.Value;
                lvItem.Tag = kvpNode;
                msgResFm.listView.Items.Add(lvItem);
            }
            if (foundList.Count != 0)
                msgResFm.Show();
            else
                msgResFm.Close();

            return foundList.Count;
        }

        private void tbSearchHistory_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void tbSearchHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearchHistory_Click(null, null);
        }

        private void tbSearchFriend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbSearchFriend_TextChanged(null, null);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {

            if (m_bShouldExit)
            {
                try
                {
                    this.Close();
                    Application.Exit();
                }
                catch (Exception ex) { }
            }
        }

        private void tbSearchHistory_Enter(object sender, EventArgs e)
        {
            if (tbSearchHistory.Text == "搜索聊天记录")
            {
                tbSearchHistory.Text = "";
                tbSearchHistory.ForeColor = Color.Black;
            }
        }

        private void tbSearchHistory_Leave(object sender, EventArgs e)
        {
            if (tbSearchHistory.Text.Trim().Length == 0)
            {
                tbSearchHistory.Text = "搜索聊天记录";
                tbSearchHistory.ForeColor = Color.LightGray;
            }
        }

    }

    // 好友信息结点
    public class FriendInfo
    {
        public string strUsrName;     // 微信ID
        public string strNickName;    // 好友原名
        public string strRemarkName;  // 好友备注
        public string strPinyin;      // 好友全拼
        public string strDisplayName; // 最终显示名
    };
}
