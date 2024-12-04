public class I_AttackRange : ItemBehaviour
{

    protected override void LevelUpEffect(int level)
    {
        switch (level)
        {
            case 1:
                playerStat.AttackRange += 8;
                break;
            case 2:
                playerStat.AttackRange += 8;
                break;
            case 3:
                playerStat.AttackRange += 8;
                break;
            case 4:
                playerStat.AttackRange += 8;
                break;
            case 5:
                playerStat.AttackRange += 8;
                break;
            case 6:
                playerStat.AttackRange += 8;
                break;
            case 7:
                playerStat.AttackRange += 8;
                break;
            case 8:
                playerStat.AttackRange += 8;
                break;
            case 9:
                playerStat.AttackRange += 16;
                break;
        }
    }

    protected override void InitExplanation()
    {
        switch (MaxLevel)
        {
            case 1:
                explanations[0] = "백신이 한번에 더 많은 바이러스를 볼 수 있게됩니다\n공격 범위를 8% 증가";
                break;
            case 2:
                explanations[1] = "공격 범위를 8% 증가";
                goto case 1;
            case 3:
                explanations[2] = "공격 범위를 8% 증가";
                goto case 2;
            case 4:
                explanations[3] = "공격 범위를 8% 증가";
                goto case 3;
            case 5:
                explanations[4] = "공격 범위를 8% 증가";
                goto case 4;
            case 6:
                explanations[5] = "공격 범위를 8% 증가";
                goto case 5;
            case 7:
                explanations[6] = "공격 범위를 8% 증가";
                goto case 6;
            case 8:
                explanations[7] = "공격 범위를 8% 증가";
                goto case 7;
            case 9:
                explanations[8] = "공격 범위를 16% 증가";
                goto case 8;
        }
    }
}