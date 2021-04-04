import { authHeader, handleResponse } from '../helpers';


export const userService = {
    GetAllProgramms,
    GetAllLevels,
    GetAllSubjects,
    GetAllDifficulties,
    GetAllLength,
    CreateAssignment,
    GetAllAssignments,
    DeleteAssignment
};

function GetAllProgramms() {
    const requestOptions = { method: 'GET', headers: authHeader() };
    return fetch(`todolist/programms`, requestOptions).then(handleResponse);
}
function GetAllLevels() {
    const requestOptions = { method: 'GET', headers: authHeader() };
    return fetch(`todolist/levels`, requestOptions).then(handleResponse);
}
function GetAllSubjects() {
    const requestOptions = { method: 'GET', headers: authHeader() };
    return fetch(`todolist/subjects`, requestOptions).then(handleResponse);
}
function GetAllDifficulties() {
    const requestOptions = { method: 'GET', headers: authHeader() };
    return fetch(`todolist/difficulties`, requestOptions).then(handleResponse);
}
function GetAllLength() {
    const requestOptions = { method: 'GET', headers: authHeader() };
    return fetch(`todolist/length`, requestOptions).then(handleResponse);
}

function GetAllAssignments() {
    const requestOptions = { method: 'GET', headers: authHeader() };
    return fetch(`todolist/assignments`, requestOptions).then(handleResponse);
}

function CreateAssignment(nameAsg, description, difficultyLevel, subjectName, lengthDur, percentage, deadline) {

    const formData = new FormData();
    formData.append("nameAsg", nameAsg);
    formData.append("description", description);
    formData.append("difficultyLevel", difficultyLevel);
    formData.append("subjectName", subjectName);
    formData.append("lengthDur", lengthDur);
    formData.append("percentage", percentage);
    formData.append("deadline", deadline);

    const requestOptions = {
        method: 'POST',
        headers: authHeader(),
        body: formData
    };

    return fetch(`todolist/CreateAssignment`, requestOptions)
        .then(handleResponse);
}

function DeleteAssignment(id) {
    const requestOptions = { method: 'DELETE', headers: authHeader() };
    return fetch(`todolist/DeleteAssignment/` + id, requestOptions).then(handleResponse);
}