﻿module MiniC.Compiler.Ast

type Program = Declaration list

and Declaration =
    | StaticVariableDeclaration of VariableDeclaration
    | FunctionDeclaration of FunctionDeclaration

and TypeSpec =
    | Void
    | Bool
    | Int
    | Float

and VariableDeclaration = 
    | ScalarVariableDeclaration of TypeSpec * Identifier
    | ArrayVariableDeclaration of TypeSpec * Identifier

and FunctionDeclaration = TypeSpec * Identifier * Parameters * CompoundStatement

and Identifier = string

and Parameters = VariableDeclaration list

and IdentifierRef(identifier : Identifier) =
    member x.Identifier = identifier
    override x.Equals obj =
        obj :? IdentifierRef && (obj :?> IdentifierRef).Identifier = identifier
    override x.GetHashCode() =
        identifier.GetHashCode()

and Statement =
    | ExpressionStatement of ExpressionStatement
    | CompoundStatement of CompoundStatement
    | IfStatement of IfStatement
    | WhileStatement of WhileStatement
    | ReturnStatement of Expression option
    | BreakStatement

and ExpressionStatement =
    | Expression of Expression
    | Nop

and CompoundStatement = LocalDeclarations * Statement list

and LocalDeclarations = VariableDeclaration list

and IfStatement = Expression (* condition *) * Statement (* then *) * Statement option (* else *)

and WhileStatement = Expression * Statement

and Expression =
    | AssignmentExpression of AssignmentExpression
    | BinaryExpression of Expression * BinaryOperator * Expression
    | UnaryExpression of UnaryOperator * Expression
    | IdentifierExpression of IdentifierRef
    | ArrayIdentifierExpression of IdentifierRef * Expression
    | FunctionCallExpression of IdentifierRef * Arguments
    | ArraySizeExpression of IdentifierRef
    | LiteralExpression of Literal
    | ArrayAllocationExpression of TypeSpec * Expression

and AssignmentExpression =
    | ScalarAssignmentExpression of IdentifierRef * Expression
    | ArrayAssignmentExpression of IdentifierRef * Expression * Expression

and BinaryOperator =
    | ConditionalOr
    | Equal
    | NotEqual
    | LessEqual
    | Less
    | GreaterEqual
    | Greater
    | ConditionalAnd
    | Add
    | Subtract
    | Multiply
    | Divide
    | Modulus

and UnaryOperator =
    | LogicalNegate
    | Negate
    | Identity

and Arguments = Expression list

and Literal =
    | BoolLiteral of bool
    | IntLiteral of int
    | FloatLiteral of float