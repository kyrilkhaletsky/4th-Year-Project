using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class LapTimeTest {

    [Test]
    public void Convert1() {
        // ARRANGE
        var time = new LapTimeManager();

        string expected = "08";
        string expected2 = "10";

        // ACT
        int x = 8;
        int y = 10;
        string num = LapTimeManager.Convert(x);
        string num2 = LapTimeManager.Convert(y);

        // ASSERT
        Assert.That(num, Is.EqualTo(expected));
        Assert.That(num2, Is.EqualTo(expected2));
    }

    [Test]
    public void Convert2() {
        // ARRANGE
        var time = new LapTimeManager();

        string expected = "00";
        string expected2 = "35";

        // ACT
        int x = 0;
        int y = 35;
        string num = LapTimeManager.Convert(x);
        string num2 = LapTimeManager.Convert(y);

        // ASSERT
        Assert.That(num, Is.EqualTo(expected));
        Assert.That(num2, Is.EqualTo(expected2));
    }

    [Test]
    public void Reset1() {
        // ARRANGE
        var time = new LapTimeManager();

        int expectedMin = 0;
        int expectedMilli = 0;
        int expectedSec = 0;
        int expectedLap = 0;

        // ACT
        LapTimeManager.minutes = 23;
        LapTimeManager.seconds = 55;
        LapTimeManager.milliseconds = 45;
        LapTimeManager.lapTime = 232685;

        LapTimeManager.ResetTimer();

        // ASSERT
        Assert.That(LapTimeManager.minutes, Is.EqualTo(expectedMin));
        Assert.That(LapTimeManager.milliseconds, Is.EqualTo(expectedMilli));
        Assert.That(LapTimeManager.seconds, Is.EqualTo(expectedSec));
        Assert.That(LapTimeManager.lapTime, Is.EqualTo(expectedLap));
    }

    [Test]
    public void Reset2() {
        // ARRANGE
        var time = new LapTimeManager();

        int expectedMin = 0;
        int expectedMilli = 0;
        int expectedSec = 0;
        int expectedLap = 0;

        // ACT
        LapTimeManager.minutes = 53;
        LapTimeManager.seconds = 2;
        LapTimeManager.milliseconds = 9;
        LapTimeManager.lapTime = 23266511585;

        LapTimeManager.ResetTimer();

        // ASSERT
        Assert.That(LapTimeManager.minutes, Is.EqualTo(expectedMin));
        Assert.That(LapTimeManager.milliseconds, Is.EqualTo(expectedMilli));
        Assert.That(LapTimeManager.seconds, Is.EqualTo(expectedSec));
        Assert.That(LapTimeManager.lapTime, Is.EqualTo(expectedLap));
    }
}
