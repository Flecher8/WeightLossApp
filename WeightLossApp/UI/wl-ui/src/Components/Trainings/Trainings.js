import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function Trainings() {
	// Trainings data
	const [trainings, setTrainings] = useState([]);
	const [training, setTraining] = useState();

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);
}

export default Trainings;
