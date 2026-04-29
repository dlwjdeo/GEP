public interface IEatable
{
    float FillAmount { get; } // 체력 회복량을 반환하는 프로퍼티
    void Eat(Player player);
}