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

class AddAssignment extends React.Component {
    _isMounted = false;
    constructor(props) {
        super(props);

        this.state = {
            subjects: [],
            difficulty: [],
            length: [],
            error: '',
        };

      
    }

    componentDidMount() {
        this._isMounted = true;
        userService.GetAllLength().then(length => this.setState({ length }));
        userService.GetAllSubjects().then(subjects => this.setState({ subjects }));
        userService.GetAllDifficulties().then(difficulty => this.setState({ difficulty }));
    }

    componentWillUnmount() {
        this._isMounted = false;
    }

    render() {
        return (
            <Formik
                initialValues={{
                    name: '',
                    deadline: new Date(),
                    description: '',
                    difficultyLevel: '',
                    percentage: '',
                    subjectName: '',
                    lengthDur: '',
                }}

                validationSchema={Yup.object().shape({
                    name: Yup.string()
                        .required("Assignment name is required."),
                    description: Yup.string()
                        .required("Description is required."),
                    difficultyLevel: Yup.string()
                        .required("Difficulty Level is required."),
                    subjectName: Yup.string()
                        .required("Subject name is required."),
                    lengthDur: Yup.string()
                        .required("Duration is required."),
                    percentage: Yup.string()
                        .required("Percentage access code."),
                    deadline: Yup.date()
                        .required("Deadline is required.")

                })}
                onSubmit={({ name, description, difficultyLevel, subjectName, lengthDur, percentage, deadline }, { setStatus, setSubmitting }) => {
                    userService.CreateAssignment(name, description, difficultyLevel, subjectName, lengthDur, percentage, deadline)
                        .then(
                            user => {
                                this.props.history.push({
                                    pathname: '/',
                                });
                            },
                            error => {
                                setSubmitting(false);
                                setStatus(error);
                            }
                        );
                }}
            >
                {({ errors, status, touched, values, setFieldValue }) => (
                    <Form style={{ marginBottom: 7 + "em" }}>

                        <Grid container
                            spacing={0}
                            direction="column"
                            alignItems="center"

                            style={{
                                minHeight: '90vh',
                            }}>
                            <Grid item xs={3} style={{ marginBottom: "2em" }}><h2>Add a task</h2></Grid>
                            <hr />
                            <Grid container spacing={8} alignItems="flex-end" xs={3}>
                               
                                <Grid item md={true} sm={true} xs={true}>
                                    <FormControl>
                                        <InputLabel htmlFor="input-with-icon-adornment">Assignment name</InputLabel>
                                        <Input
                                            id="input-with-icon-adornment"
                                            onChange={name => setFieldValue('name', name.target.value)}
                                            name="name"
                                            startAdornment={
                                                <InputAdornment position="start">
                                                    <NoteIcon />
                                                </InputAdornment>
                                            }
                                        />

                                    </FormControl>

                                    <ErrorMessage name="name" component="div" className="invalid-feedback" />
                                </Grid>

                            </Grid>
                            <Grid container spacing={8} alignItems="flex-end" xs={3} style={{ marginTop: "-0.2em" }}>
                                <Grid item md={true} sm={true} xs={true}>
                                    <TextareaAutosize aria-label="minimum height" rowsMin={3} style={{width: 100 + "%"}} label="Description:" placeholder="Enter description..." onChange={description => setFieldValue('description', description.target.value)} />
                                    <ErrorMessage name="description" component="div" className="invalid-feedback" />
                                </Grid>

                            </Grid>


                            <Grid container spacing={8} alignItems="flex-end" xs={3} style={{ marginTop: "-0.2em" }}>
                   
                                <Grid item md={true} sm={true} xs={true}>
                                    <TextField
                                        id="standard-number"
                                        label="Percentage"
                                        type="number"
                                        InputProps={{ inputProps: { min: 1, max: 100 } }}
                                        onChange={percentage => setFieldValue('percentage', percentage.target.value)}
                                        InputLabelProps={{
                                            shrink: true,
                                        }}
                                    />
                                    <ErrorMessage name="percentage" component="div" className="invalid-feedback" />
                                </Grid>

                            </Grid>
                          
               
                            <Grid container spacing={8} alignItems="flex-end" xs={3} style={{ marginTop: "1em" }}>
                                <Grid item md={true} sm={true} xs={true}>
                                    <Autocomplete
                                        id="combo-box-demo"
                                        options={this.state.difficulty}
                                        getOptionLabel={(option) => option.name}
                                        style={{ width: 300 }}
                                        renderInput={(params) => <TextField {...params} label="Difficulty Level" variant="outlined" />}
                                        onChange={(event, value) => setFieldValue('difficultyLevel', value.value)}
                                    />

                                    <ErrorMessage name="difficultyLevel" component="div" className="invalid-feedback" />
                                </Grid>
                            </Grid>


                            <Grid container spacing={8} alignItems="flex-end" xs={3} style={{ marginTop: "1em" }}>
                                <Grid item md={true} sm={true} xs={true}>
                                    <Autocomplete
                                        id="combo-box-demo"
                                        options={this.state.length}
                                        getOptionLabel={(option) => option.name}
                                        style={{ width: 300 }}
                                        renderInput={(params) => <TextField {...params} label="Length" variant="outlined" />}
                                        onChange={(event, value) => setFieldValue('lengthDur', value.value)}
                                    />

                                    <ErrorMessage name="lengthDur" component="div" className="invalid-feedback" />
                                </Grid>
                            </Grid>



                            <Grid container spacing={8} alignItems="flex-end" xs={3} style={{ marginTop: "1em" }}>
                                <Grid item md={true} sm={true} xs={true}>
                                    <Autocomplete
                                        id="combo-box-demo"
                                        options={this.state.subjects}
                                        getOptionLabel={(option) => option.name}
                                        style={{ width: 300 }}
                                        renderInput={(params) => <TextField {...params} label="Subject" variant="outlined" />}
                                        onChange={(event, value) => setFieldValue('subjectName', value.value)}
                                    />

                                    <ErrorMessage name="subjectName" component="div" className="invalid-feedback" />
                                </Grid>
                            </Grid>


                            <Grid container spacing={8} alignItems="flex-end" xs={3} style={{ marginTop: "1em" }}>
                                <Grid item md={true} sm={true} xs={true}>
                                    <TextField
                                        id="date"
                                        label="Deadline"
                                        type="datetime-local"
                                        InputLabelProps={{
                                            shrink: true,
                                        }}
                                        onChange={deadline => setFieldValue('deadline', deadline.target.value)}
                                    />

                                    <ErrorMessage name="deadline" component="div" className="invalid-feedback" />
                                </Grid>
                            </Grid>



                            <Grid container spacing={8} justify="flex-end" xs={3} style={{ marginTop: "2em", marginLeft: "-6.5em" }}>
                                <Button type="submit" variant="contained" color="secondary">
                                    Add
                                    </Button>


                                {status &&
                                    <div className={'alert alert-danger'}>{status}</div>
                                }
                            </Grid>
                        </Grid>



                    </Form>
                )}
            </Formik>
        )
    }
}

export { AddAssignment };