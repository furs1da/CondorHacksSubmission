import React from 'react';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import DatePicker, { registerLocale } from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { authenticationService } from '../services';
import { userService } from '../services';
import PasswordField from 'material-ui-password-field';
import InputAdornment from '@material-ui/core/InputAdornment';
/* eslint-disable */
import Input from '@material-ui/core/Input';
import InputLabel from '@material-ui/core/InputLabel';
import FormControl from '@material-ui/core/FormControl';
import FaceIcon from '@material-ui/icons/Face';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import MailOutlineIcon from '@material-ui/icons/MailOutline';
import MeetingRoomIcon from '@material-ui/icons/MeetingRoom';
import Autocomplete from '@material-ui/lab/Autocomplete';
import TextareaAutosize from '@material-ui/core/TextareaAutosize';
import NoteIcon from '@material-ui/icons/Note';
import Card from '@material-ui/core/Card';
import DeleteForeverIcon from '@material-ui/icons/DeleteForever';
import { confirmAlert } from "react-confirm-alert";
import "react-confirm-alert/src/react-confirm-alert.css";

class ListAssignment extends React.Component {
    _isMounted = false;
    constructor(props) {
        super(props);

        this.state = {
            assignments: [],     
            error: '',
        };


    }

    componentDidMount() {
        this._isMounted = true;
        userService.GetAllAssignments().then(assignments => this.setState({ assignments }));
    }

    componentWillUnmount() {
        this._isMounted = false;
    }
    DeleteRecordConfirmed(idChild) {
        console.log(idChild);
        userService.DeleteAssignment(idChild).then(response => {
            userService.GetAllAssignments().then(assignments => this.setState({ assignments }));
        }).catch(error => this.setState({ error }));
    }

    DeleteRecord(idChild) {
        confirmAlert({
            title: "Confirm the action",
            message: "Are you sure that you want to delete this task?",
            buttons: [
                {
                    label: "Yes",
                    onClick: () => { this.DeleteRecordConfirmed(idChild) }
                },
                {
                    label: "No"
                }
            ]
        });
    };

    onRedirectToChange() {
        this.props.history.push({
            pathname: '/addTask'
        });
    }

    render() {
        return (
            <Formik>
                {({ errors, status, touched, values, setFieldValue }) => (
                    <Form style={{ marginBottom: 7 + "em" }}>

                        <Grid container
                            spacing={0}
                            direction="column"
                            alignItems="center"

                            style={{
                                minHeight: '90vh',
                            }}>
                            <Grid item xs={3} style={{ marginBottom: "2em" }}><h2>List of tasks</h2></Grid>
                            <hr />
                            <Grid container spacing={8} alignItems="flex-end" xs={3}>
                                {this.state.assignments.map(assgnEntity =>
                                    <div class="card" style={{ marginTop: 1.5 + "em", marginBottom: 1 + "em", marginLeft: 1 + "em" }}>
                                        <div class="card-header">
                                            Assignment name: {assgnEntity.name}
                                        </div>
                                        <div class="card-body">
                                            <h5 class="card-title">Deadline: {new Date(assgnEntity.deadline).toLocaleDateString()}</h5>
                                            <h5 class="card-title">Subject: {assgnEntity.idSubject}</h5>
                                            <h5 class="card-title">Done: {assgnEntity.done}</h5>
                                            <h5 class="card-title">Difficulty: {assgnEntity.difficulty}</h5>
                                            <h5 class="card-title">Length: {assgnEntity.length}</h5>
                                            <h5 class="card-title">Percent Of the Final Grade: {assgnEntity.percentOfFinalGrade}</h5>
                                            <br />
                                            <h6 class="card-subtitle mb-2 text-muted">Description:</h6>
                                            <p class="card-text"> {assgnEntity.description}</p>   
                                            <Button type="submit" variant="contained" color="secondary" onClick={selectValue => this.DeleteRecord(assgnEntity.idAssignment)}>
                                                <DeleteForeverIcon />
                                            </Button>
                                        </div>
                                    </div>
                                )}
                                    <ErrorMessage name="deadline" component="div" className="invalid-feedback" />
                                </Grid>
                            </Grid>
                    </Form>
                )}
            </Formik>
        )
    }
}

export { ListAssignment };