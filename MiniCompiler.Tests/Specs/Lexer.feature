Feature: ReservedWords
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Input is Empty
	Given I have an input of ''
	When We Tokenize
	Then the result should be 
		| Type		| Lexeme   | Column | Row |
		| EOF	    | $	       |   0    | 0   |

Scenario: Input is an id equal to haloword
	Given I have an input of 'haloworld'
	When We Tokenize
	Then the result should be 
		| Type		| Lexeme   | Column | Row |
		| Id	    | haloworld|   0    | 0   |
		| EOF	    | $	       |   9    | 0   |
	
Scenario: Input is 2 ids with whitespaces
	Given I have an input of 'halo warudo'
	When We Tokenize
	Then the result should be 
		| Type		| Lexeme   | Column | Row |
		| Id	    | halo     |   0    | 0   |
		| Id	    | warudo   |   5    | 0   |
		| EOF	    | $	       |   11   | 0   |

Scenario: Input is an int equal to 2042
	Given I have an input of '2042'
	When We Tokenize
	Then the result should be 
		| Type		| Lexeme   | Column | Row |
		| Number	| 2042     |   0    | 0   |
		| EOF	    | $	       |   4    | 0   |

Scenario: Input is an int and an a 2042
	Given I have an input of '2042a'
	When We Tokenize
	Then the result should be 
		| Type		| Lexeme   | Column | Row |
		| Number	| 2042     |   0    | 0   |
		| Id		| a		   |   4    | 0   |
		| EOF	    | $	       |   5    | 0   |

Scenario: Input is an id a2042
	Given I have an input of 'a2042'
	When We Tokenize
	Then the result should be 
		| Type		| Lexeme   | Column | Row |
		| Id		| a2042	   |   0    | 0   |
		| EOF	    | $	       |   5    | 0   |