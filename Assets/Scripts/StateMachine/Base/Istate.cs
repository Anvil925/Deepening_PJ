public interface IState
{
    void Enter(); // 초기화
    void Execute(); // 매 프레임 실행
    void Exit(); // 정리
}
