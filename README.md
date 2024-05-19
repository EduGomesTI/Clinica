# Sistema de Agendamento de Consultas Médicas #

## Requisitos Funcionais Obrigatórios ##

- Os(as) pacientes devem ser capazes de criar e gerenciar contas
pessoais.
- Os(as) pacientes devem ser capazes de agendar, reagendar ou cancelar
consultas.
- Os(as) médicos(as) devem ser capazes de visualizar e gerenciar suas
agendas de consultas.
- Deve haver um sistema de notificação para lembrar pacientes de suas
consultas. Estas notificações podem ser via e-mail.

## Outros Requisitos Funcionais ##

- Uma consulta realizada vira um item no prontuário (se implementado).
- Uma consulta agendada cria um agendamento para o médico.
- Um médico terá apenas 1 especialidade (Simplificação).
- O médico confirma a consulta na agenda e o status no agendamento é alterado.
- Cada consulta tem uma duração fixa de 30 minutos.
- O padrão para exibição do horário das consultas é xxhxx, ex: 07h30, 18h00, etc.
- O horário para agendamento de consultas é de Segunda a Sábado das 07h00 até às 21h00.
- O endereço dos pacientes será simplificado.

## Requisitos Não Funcionais Obrigatórios ##

- O sistema deve ser desenvolvido utilizando C# e .NET.
- A interface do usuário fica a critério do(a) estudante.
- O sistema deve ter um fluxo de delete lógico não físico.
-
## Outros Requisitos Não Funcionais ##

- Classes, métodos e outras partes em Inglês.
- ORM: EF.
- Message Broker: RabbitMQ.

## Arquitetura ##

- Monolito Modular.
- Módulos:
  - Main: Entrada da Aplicação com Endpoints HTTP REST.
  - Base: Padrões adotados ao longo do projeto por todos os módulos.
  - Pacients: Gerenciamento de pacientes e Agendamentos de Consultas.
  - Doctors: Gerenciamento dos médicos, especialidades e agendas.
- Cada módulo será desacoplado e terá comunicação assíncrona com os demais, com exceção do módulo Base que poderá ser usado com qualquer outro módulo.
- Cada módulo terá como arquitetura padrão Clean Architecture. O módulo Main e Base não terão arquitetura definida.
- Será usado algumas patterns de DDD como agregados e contextos delimitados conforme a necessidade.
- A persistência de dados será implementada com o pattern Repository.
- A criação dos objetos será feito com o pattern Factory dentro de suas respectivas classes.
- Na medida do possível cada classe implementará todas as suas regras de negócios e validações.
- As validações dos Dtos será feita na camada Application com exceção da Main.

## Endpoints ##

### Pacientes ###

```
POST /api/v1/patient/
{
  "name": string,
  "bornDate": DateTime,
  "email": string,
  "phone": string,
  "address": string
}
```
```
PATCH /api/v1/patient/
{
  "id": GUID
  "name": string,
  "bornDate": DateTime,
  "email": string,
  "phone": string,
  "address": string
}
```
```
(Delete/Undelete) PATCH /api/v1/patient/
{
  "idPatient": GUID,
  "isDeleted": bool
}
```
```
GET /api/v1/patient/?id
```
```
GET /api/v1/patient
```
### Agendamento ###

```
POST /api/v1/scheduling/
{
  "idPatient": GUID,
  "idDoctor": GUID,
  "dayScheduling": byte,
  "hourScheduling": string
}
```
```
PATCH /api/v1/scheduling/
{
  "id": GUID,
  "idPatient": GUID,
  "idDoctor": GUID,
  "dayScheduling": byte,
  "hourScheduling": string,
  "status": string
}
```
```
GET /api/v1/scheduling/?id
```
```
GET /api/v1/scheduling/?idPatient
```
```
GET /api/v1/scheduling/?idDoctor
```
```
GET /api/v1/scheduling
```
### Medicos ###

```
POST /api/v1/doctor/
{
  "name": string,
  "bornDate": DateTime,
  "email": string,
  "phone": string,
  "address": string
}
```
```
PATCH /api/v1/doctor/
{
  "id": GUID
  "name": string,
  "crm": string,
  "idSpecialty: GUID
}
```
```
(Delete/Undelete) PATCH /api/v1/doctor/
{
  "idDoctor": GUID,
  "isDeleted": bool
}
```
```
GET /api/v1/doctor/?id
```
```
GET /api/v1/doctor
```

### Especialidades ###
```
POST /api/v1/specialty
{
  "specialty" : string
}
```
```
PATCH /api/v1/specialty/
{
  "id": GUID,
  "specialty" : string
}
```
```
(Delete/Undelete) PATCH /api/v1/specialty/
{
  "idSpecialty": GUID,
  "isDeleted": bool
}
```
```
GET /api/v1/specialty/?id
```
```
GET /api/v1/specialty
```

### Agenda dos Medicos ###
```
POST /api/v1/doctorSchedule
{
  [
    {
    "weekDay": string,
    "hoursDay": [string]
    }
  ]
}
```
```
PATCH /api/v1/doctorSchedule/
{
  "idDoctorSchedule": GUID
  "weekDay": string,
  "hourDay": string
}
```
```
(Delete/Undelete) PATCH /api/v1/doctorSchedule/
{
  "idDoctorSchedule": GUID,
  "isDeleted": bool
}
```
```
GET /api/v1/doctorSchedule/?id
```
```
GET /api/v1/doctorSchedule/?idDoctor
```