# Expectativa Mercado Mensal
Aplicativo feito com WPF para listar os resultados do endpoint https://olinda.bcb.gov.br/olinda/servico/Expectativas/versao/v1/odata/ExpectativaMercadoMensais através de uma chamada assíncrona em um Data Grid.

## Instruções

1. Selecione um indicador no controle responsável que está abaixo do texto indicador.
2. Insira uma Data inicial no controle que está abaixo do texto indicando a data inicial.
3. Insira uma Data final no controle que está abaixo do texto indicando a data final.
4. Clique no botão com o texto filtrar e espere a tabela ser preenchida.
5. A tabela com o data grid só pode apresentar no máximo 100 registros por vez, divididos em páginas. Então, para selecionar mais registros clique no botão Anterior para filtrar os registros da página anterior ou no botão Próximo para filtrar os registros na página posterior.
6. É possível exportar os registros da tabela atual para um arquivo .csv. Para isto, clique no botão com o texto indicando a exportação para csv e preencha o nome na caixa que se abrirá, selecionando a pasta em que deseja salvar o arquivo. O arquivo tem um nome padrão: [nome do indicador]-[data inicial]-[data final]-[página atual].
7. Quando for navegar entre as páginas, não altere os valores nos campos de entrada. Pois eles são usados para filtrar os registros.