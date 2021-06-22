select * from TBTarefas

insert into TBTarefas
	(
	Titulo, 
	DataCriação, 
	DataConclusão, 
	PercentualConclusão, 
	Prioridade
	) 
	values 
	(
	'Tarefa Academia', 
	'06/18/2021', 
	null, 
	90, 
	'Alta'
	)

update TBTarefas 
	set	
		[Titulo] = 'TrabalhoFaculdade', 
		[DataCriação]='06/10/2021', 
		[DataConclusão] = '06/15/2021',
	    [PercentualConclusão] = 100	

	where 
		[Id] = 1

Delete from TBTarefas 
	where 
		[Id] = 1


select [Id], [Titulo], [DataCriação], [DataConclusão], [PercentualConclusão] from TBTarefas