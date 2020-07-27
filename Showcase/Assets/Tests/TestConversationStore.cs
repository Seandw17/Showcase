using NUnit.Framework;
using System;

namespace Tests
{
    public class TestConversationStore
    {
        [Test]
        public void TestAddTip()
        {
            ConversationStore.AddTip(e_tipCategories.CRITICISM);
            Assert.AreEqual(
                ConversationStore.GetPlayerTips(), e_tipCategories.CRITICISM);

            ConversationStore.AddTip(e_tipCategories.ENTHUSIASM);
            Assert.IsTrue(ConversationStore.GetPlayerTips().HasFlag(e_tipCategories.ENTHUSIASM));
            Assert.IsTrue(ConversationStore.GetPlayerTips().HasFlag(e_tipCategories.CRITICISM));
            Assert.IsFalse(ConversationStore.GetPlayerTips().HasFlag(e_tipCategories.NOTASKING));
        }

        [Test]
        public void TestUnlockFlags()
        {
            ConversationStore.RegisterUnlockFlag(e_unlockFlag.FIRST);

            Assert.IsTrue(ConversationStore.CheckHasFlag(e_unlockFlag.FIRST));

            Assert.IsFalse(ConversationStore.CheckHasFlag(e_unlockFlag.SECOND));
        }

        [Test]
        public void TestLookedAway()
        {
            for (int index = 0; index < 5; index++)
            {
                ConversationStore.LookedAway();
            }

            Assert.AreEqual(ConversationStore.GetLookedAway(), 5);

            ConversationStore.LookedAway();

            Assert.AreNotEqual(ConversationStore.GetLookedAway(), 6);
        }

        [Test]
        public void TestArrivedOnTime()
        {
            ConversationStore.DidntArrivedInWaitingAreaOnTime();
            ConversationStore.DidntArrivedToShopOnTime();

            Assert.AreEqual(ConversationStore.GetDidntArrivedOnTime(), 2);

            ConversationStore.DidntArrivedToShopOnTime();

            Assert.AreNotEqual(ConversationStore.GetDidntArrivedOnTime(), 3);
        }
    }
}
