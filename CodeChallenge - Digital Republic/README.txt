CodeChallenge - Digital Republic

Para executar este projeto são necessários:
- IDE com linguagem Java.
- Gerênciador de Banco de Dados, preferêncialmente MySQL.
- API Client ou alguma ferrameta para fazer requisições (Postman, por exemplo)

Primeiro é necessário iniciar o banco de dados, (O programa vai usar a porta 127.0.0.1:3306 então o banco de dados
precisar estar nessa porta. Basta então executar o programa usando a IDE, abrir o API Client/Postman, e começar a
fazer as requisições para o programa.

O programa recebe quatro requisições:

POST /cadastrar Body{ "nome":string, "cpf":string } - Cadastra um cliente usando as informações dadas no json.

POST /depositar Body{ "cpf":string, "valor":int } - Deposita o valor na conta do CPF correspondende. (desde que
o valor não seja maior que 2000)

POST /transferir Body{ "cpf":string, "cpfDestino":string, "valor":int } - Transfere o valor estipulado da conta
com CPF correspondente para a conta com CPFDestino correspondente.

GET /saldo/{cpf} - Retorna um json com o nome, CPF e saldo da conta com CPF correspondente à {cpf}.
