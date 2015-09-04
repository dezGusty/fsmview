using FiniteStateMachine.Machine;
using FiniteStateMachineTest.Graph;
using FiniteStateMachineTest.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachineTest.DomainModel
{
    public class StateMachine
    {
        public StateMachine()
        {
        }
        public readonly List<string> MachineStates = new List<string>();
        public readonly List<char> Alfabet = new List<char>();
        public readonly List<Transition> Delta = new List<Transition>();
        public string Q0;
        public readonly List<string> FinishStates = new List<string>();

        /// <summary>
        /// Adds the list of transitions to this Finite state machine transitions.
        /// </summary>
        /// <param name="transitions">The transitions.</param>
        private void AddTransitions(IEnumerable<Transition> transitions)
        {
            foreach (var transition in transitions.Where(ValidTransition))
            {
                Delta.Add(transition);
            }
        }

        /// <summary>
        /// Valids the transition.
        /// </summary>
        /// <param name="transition">The transition.</param>
        /// <returns></returns>
        private bool ValidTransition(Transition transition)
        {
            return MachineStates.Contains(transition.StartState) &&
                    MachineStates.Contains(transition.EndState) &&
                    Alfabet.Contains(transition.Token) &&
                    !TransitionAlreadyDefined(transition);
        }

        /// <summary>
        /// Verifies if the transition is not defined.
        /// </summary>
        /// <param name="transition">The transition to be verified.</param>
        /// <returns></returns>
        private bool TransitionAlreadyDefined(Transition transition)
        {
            return Delta.Any(t => t.StartState == transition.StartState &&
                                  t.Token == transition.Token);
        }

        /// <summary>
        /// Adds the initial state.
        /// </summary>
        /// <param name="q0">The q0.</param>
        private void AddInitialState(string q0)
        {
            if (MachineStates.Contains(q0))
            {
                Q0 = q0;
            }
        }

        /// <summary>
        /// Adds the final states.
        /// </summary>
        /// <param name="finalStates">The final states.</param>
        private void AddFinalStates(IEnumerable<string> finalStates)
        {
            foreach (var finalState in finalStates.Where(
                       finalState => MachineStates.Contains(finalState)))
            {
                FinishStates.Add(finalState);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FiniteStateMachine"/> class.
        /// </summary>
        /// <param name="q">The q.</param>
        /// <param name="sigma">The sigma.</param>
        /// <param name="delta">The delta.</param>
        /// <param name="q0">The inital state.</param>
        /// <param name="f">The fina states.</param>
        public StateMachine(IEnumerable<string> q, IEnumerable<char> sigma,
                            IEnumerable<Transition> delta, string q0,
                            IEnumerable<string> f)
        {
        MachineStates = q.ToList();
        Alfabet = sigma.ToList();
        AddTransitions(delta);
        AddInitialState(q0);
        AddFinalStates(f);
       }

        public String Accepts(string input)
        {
            var currentState = Q0;
            var steps = new StringBuilder();
            foreach (var symbol in input.ToCharArray())
            {
                var transition = Delta.Find(t => t.StartState == currentState &&
                                                 t.Token == symbol);
                if (transition == null)
                {
                    return "No transitions for current state and symbol";

                }
                currentState = transition.EndState;
                steps.Append(transition + "\n");
            }
            if (FinishStates.Contains(currentState))
            {
                return "Accepted the input with steps:\n" + steps;

            }
            return "Stopped in state " + currentState +" which is not a final state.";
        }

        public List<PairOfStates> GetEdges(string input)
        {
            List<PairOfStates> list = new List<PairOfStates>();
            var currentState = Q0;
            var steps = new StringBuilder();
            foreach (var symbol in input.ToCharArray())
            {
                var transition = Delta.Find(t => t.StartState == currentState &&
                                                 t.Token == symbol);

                if(transition!=null)
                {
                    string state1 = currentState;
                    string state2 = transition.EndState;
                    list.Add(new PairOfStates(state1, state2));
                    currentState = transition.EndState;
                    steps.Append(transition + "\n");
                }
            }
            return list;
        }
    }
}
