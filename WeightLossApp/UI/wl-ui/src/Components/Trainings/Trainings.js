import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function Trainings() {
	// Trainings data
	const [trainings, setTrainings] = useState([]);
	const [training, setTraining] = useState();

	// SectionTraining data
	const [sectionTraining, setSectionTraining] = useState([]);

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

	// Get trainings data
	function getTrainings() {
		fetch(constants.API_URL + "Training")
			.then(res => res.json())
			.then(data => setTrainings(data));
	}

	// Get SectionTraining data
	function getSectionTraining() {
		fetch(constants.API_URL + "SectionTraining")
			.then(res => res.json())
			.then(data => setSectionTraining(data));
	}

	// onLoad function
	useEffect(() => {
		getTrainings();
		getSectionTraining();
	}, []);

	// Show add modal function
	function addModalShow() {
		addHandleShow();
		setTraining({
			Id: undefined,
			Section: 0,
			Complexity: ""
		});
	}

	// Show edit modal function
	function editModalShow(training) {
		editHandleShow();
		setTraining(training);
	}

	// Delete item function
	function deleteClick(id) {
		if (window.confirm("Are you sure?")) {
			fetch(constants.API_URL + `Training/${id}`, {
				method: "DELETE",
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				}
			})
				.then(res => res.json())
				.then(
					result => {
						getTrainings();
					},
					error => {
						alert("Failed");
					}
				);
		}
	}

	// Get section name function
	function getSectionType(id) {
		for (let i = 0; i < sectionTraining.length; i++) {
			if (sectionTraining[i].Id === id) {
				return sectionTraining[i].Type;
			}
		}
		return "NO SECTION TYPE";
	}

	return (
		<div className="Trainings">
			{/* Create new item button */}
			<div className="container mt-5" align="right">
				<Button onClick={() => addModalShow()} variant="outline-primary">
					Create new training
				</Button>
			</div>
			{/* Main table */}
			<div className="container mt-5">
				<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
					<thead>
						<tr>
							<th>
								ID
								<Button className="btn-light">
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Section
								<Button className="btn-light">
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Complexity
								<Button className="btn-light">
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th></th>
						</tr>
					</thead>
					<tbody>
						{trainings.map(e => (
							<tr key={e.Id}>
								<td>{e.Id}</td>
								<td>{getSectionType(e.Section)}</td>
								<td>{e.Complexity}</td>
								<td>
									<Button onClick={() => editModalShow(e)} variant="outline-dark">
										Edit
									</Button>
									<Button onClick={() => deleteClick(e.Id)} variant="outline-danger">
										Delete
									</Button>
								</td>
							</tr>
						))}
					</tbody>
				</Table>
			</div>
		</div>
	);
}

export default Trainings;
