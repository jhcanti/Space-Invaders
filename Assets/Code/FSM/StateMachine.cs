using System;
using System.Collections.Generic;

public class StateMachine
{
    // tenemos 3 listas de transiciones:
    // _transitions contiene todas las transiciones
    // _currentTransitions contiene solo las transiciones del estado actual
    // _anyTransitions contiene las transiciones que no requieren de un estado inicial
    private IState _currentState;
    private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> _currentTransitions = new List<Transition>();
    private List<Transition> _anyTransitions = new List<Transition>();
        
    private static List<Transition> EmptyTransitions = new List<Transition>(0);

    // metodo que se llamara desde el Update en cada frame
    // primero intenta recuperar una transición
    // si la encuentra hace la transicion hacia el nuevo estado
    // por ultimo llama al metodo Tick del estado actual
    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
            SetState(transition.To);
            
        _currentState?.Tick();
    }

    // si el estado al que queremos ir es el mismo en el que ya estamos no hacemos nada
    // primero ejecutamos OnExit del estado actual
    // despues recuperamos las transiciones del estado al que queremos ir y las guardamos en _currentTransitions
    // despues actualizamos el estado actual
    // por ultimo ejecutamos OnEnter del nuevo estado
    public void SetState(IState state)
    {
        if (state == _currentState)
            return;
            
        _currentState?.OnExit();
        _currentState = state;

        _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        if (_currentTransitions == null)
            _currentTransitions = EmptyTransitions;
            
        _currentState.OnEnter();
    }

    // metodo que añade una transicion a la lista de transiciones totales _transitions
    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }
            
        transitions.Add(new Transition(to, predicate));
    }

    // metodo que añade una transicion a la lista de transiciones que no dependen de un estado _anyTransitions
    public void AddAnyTransition(IState state, Func<bool> predicate)
    {
        _anyTransitions.Add(new Transition(state, predicate));
    }
        
    // clase que define una Transicion
    private class Transition
    {
        public Func<bool> Condition { get; }
        public IState To { get; }

        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    // metodo que devuelve la primera transicion que cumpla su condicion
    // primero busca entre las transiciones de _anyTransitions
    // despues busca entre las transiciones de _currentTransitions
    // si no encuentra ninguna que cumpla sus condiciones devuelve null
    private Transition GetTransition()
    {
        foreach (var transition in _anyTransitions)
        {
            if (transition.Condition())
                return transition;
        }

        foreach (var transition in _currentTransitions)
        {
            if (transition.Condition())
                return transition;
        }

        return null;
    }
}
