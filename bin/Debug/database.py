from sqlalchemy import create_engine, Column, Integer, String, ForeignKey
from sqlalchemy.orm import declarative_base, relationship, sessionmaker

Base = declarative_base()

class Status(Base):
    __tablename__ = 'Status'

    statusId = Column(Integer, primary_key=True, autoincrement=True)
    status = Column(String)


class Student(Base):
    __tablename__ = 'Student'

    studentId = Column(Integer, primary_key=True)
    naam = Column(String, name='voornaam')  # Update to match your actual column name
    secondname = Column(String, name='achternaam')  # Update to match your actual column name
    email = Column(String)
    statusId = Column(Integer, ForeignKey('Status.statusId'))
    status = relationship('Status', back_populates='students')

Status.students = relationship('Student', back_populates='status')

def insert_students(records):
    engine = create_engine('sqlite:///db.db')
    Base.metadata.create_all(engine)
    Session = sessionmaker(bind=engine)
    session = Session()

    try:
        for student_data in records:
            status = Status(status=student_data[3])
            student = Student(naam=student_data[0], secondname=student_data[1], email=student_data[2], status=status)
            session.add_all([status, student])

        session.commit()
    finally:
        session.close()

if __name__ == "__main__":
    students = [
        ("Huma", "Raja Liaqat", "r0933322", "Active"),
        ("John", "Doe", "john.doe@example.com", "Inactive"),
        # Add more records as needed
    ]

    insert_students(students)
