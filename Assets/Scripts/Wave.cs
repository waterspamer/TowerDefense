public class Wave
{
    private int enemyHealth;

    private int enemyCount;

    public int GetEnemyHealth()
    {
        return enemyHealth;
    }
    public int GetEnemyCount()
    {
        return enemyCount;
    }

    public Wave (int enemyHealth, int enemyCount)
    {
        this.enemyHealth = enemyHealth;
        this.enemyCount = enemyCount;
    }
}
