# Value

is a pico library (or code snippets shed) to help you to __easily implement Value Types__ in your C# projects without polluting your domain logic with boiler-plate code.

![Value](https://github.com/tpierrain/Value/blob/master/Value-small.jpg?raw=true)

## Value Types?
__Domain Driven Design (DDD)__'s *Value Object* being an oxymoron (indeed, an object has a changing state by nature), we now rather use the "*Value Type*" (instance) terminology. But the concept is the same as described within Eric Evan's Blue book.

__A Value Type is:__
 - __immutable__ (every field must be read-only after the Value Type instantiation; no 'setter' is allowed)
 - __rich with domain logic and behaviours__. The idea is to swallow (and encapsulate) most of our business complexity within those classes
 - __100% Ubiquitous Language__: Cure to primitive obsession, the usage of Value Types is an opportunity for us to embrace the language of our business within our code base
 - __function-oriented__ Our domain logic will be implemented by exposing, using and combining functions (usually returning new instance(s) of Value Type; following 'closure of operations' if the type returned is of the same type)
 - __providing Equality and Uniqueness based on ALL its attributes__ (and "all" is *a must* here)
 - __auto-validating__ (via *transactional* constructors __with business validation inside__)


### Reverse the trend (Value Types vs. Entities)

Even if the category you pick will strongly depends on your business context, here is some basics sample to clarify between *Value Types* and *Entities*:
 
 - __*Value types*__: cards of a poker hand, a speed of 10 mph, a bank note of 10 euros (unless you are working for a Central Bank which need then to trace every bank notes --> Entity), a user address

 - __*Entities*__: a user account, a customer's basket with items, a customer, ...

Our OO code bases are usually full of types with states and contain a very few Value Type instances.
DDD advises us to reverse the trend by not having *Entities* created by default, and to increase our __usage of Value Types__. 

This __will helps us to reduce side-effects within our OO base code__. A simple reflex, for great benefits.

## Side effects, you said?

Yes, because one of the problem we face when we code with Object Oriented (OO) languages like C# or java is the presence of __side-effects__. Indead, the ability for object instances to have their own state changed by other threads or by a specific combination of previous method calls (temporal coupling) __makes our reasoning harder__. Doing Test Driven Development helps a lot, but is not enough to ease the reasoning about our code.

Being inspired by functional programming (FP) languages, __DDD suggests us to make our OO design more FP oriented in order to reduce those painful side-effects__. They are many things we can do for it. E.g.: 
 - to use and combine __functions__ instead of methods that impact object states
 - to embrace __CQS pattern__ for *Entity* objects (i.e. a paradigm where read methods never change state and write methods never return data)
 - to implement *Closure of Operations* whenever it's possible (to reduce coupling with other types)
 - to use __*Value Types*__ by default and to keep *Entity* objects only when needed. An *Entity* is a object that has a changeable state (often made by combining Value Objects) for which we care about its identity.

Since there is no first-class citizen for immutability and *Value Types* in C#, the goal of this pico library is to help you easily implement Value Types without caring too much on the boiler-plate code. 

__Yeah, let's focus on our business value now!__

--- 

## What's inside the box?

E.g.: 

 - __ValueType<T>__: making all your Value Types deriving from this base class will allow you to properly implement Equality (IEquatable) and Unicity (GetHashCode()) on ALL your fields _in 1 line of code_. Very Handy!
```c#
    // 1. You inherit from ValueType<T> like this:
	public class Amount : ValueType<Amount>
    {
        private readonly decimal quantity;
        private readonly Currency currency;
		
		...

		// 2. You (are then forced to) implement the abstract method returning the list of all your fields
		protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return new List<object>() { this.quantity, this.currency }; // The line of code I was talking about
        }

		// And that's all folks!
    }


```

 - __ListByValue<T>__: a list supporting equality based on its content and not on its reference (i.e.: 2 different instances containing the same items will be equals). This collection is __very useful for any ValueType that would like to aggregate a list__

 - ...

 
 
