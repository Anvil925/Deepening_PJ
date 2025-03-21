public class StateMachine
{
    private IState _currentState; // 현재 상태
    
    public IState CurrentState => _currentState; // 현재 상태 반환
    
    public void ChangeState(IState newState) // 상태 전환
    {
        if (_currentState != null) // 현재 상태가 null이 아니면
            _currentState.Exit(); // 현재 상태 종료
            
        _currentState = newState; // 현재 상태를 새 상태로 변경
        _currentState.Enter(); // 새 상태 시작
    }
    
    public void Update() 
    {
        if (_currentState != null) // 현재 상태가 null이 아니면
            _currentState.Execute(); // 현재 상태 실행
    }
}