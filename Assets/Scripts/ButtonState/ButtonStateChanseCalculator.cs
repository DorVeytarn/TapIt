using UnityEngine;

public class ButtonStateChanseCalculator
{
    private int[] numberSeries;
    private int bitCalculationInterval = 2;
    private int currentBitNumber = 0;
    private int currentSeriesIndex = 0;

    public ButtonStateChanseCalculator(int[] numberSeries)
    {
        this.numberSeries = numberSeries ?? throw new System.ArgumentNullException(nameof(numberSeries));
    }

    public bool CalculateChanse()
    {
        if(currentBitNumber == bitCalculationInterval)
        {
            float randomSeed = Random.Range(0f, 1f);
            float currrentNumber = numberSeries[currentSeriesIndex] / 10f;
            bool nextState = randomSeed >= currrentNumber;

            currentSeriesIndex++;
            if (currentSeriesIndex > numberSeries.Length - 1)
                currentSeriesIndex = 0;

            currentBitNumber = 0;

            return nextState;
        }
        else
        {
            currentBitNumber++;

            return true;
        }
    }
}
